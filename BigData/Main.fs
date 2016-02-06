namespace Application2

open WebSharper
open WebSharper.Sitelets
open WebSharper.UI.Next
open WebSharper.UI.Next.Server


type T5PMenu =
    {   
        menucode : string
        english : string                
        submenu : string
        menulevel : int
}

type Menu =
    {   menuorder : int            
        english : string
        chinese : string
        menucode : string
        submenu : string
}

type Node = {
    menucode : string;
    Description: string;
    mutable Children: seq<Node>
}

type EndPoint =
    | [<EndPoint "/">] Home
    | [<EndPoint "/about">] About
    | [<EndPoint "/process">] ProcessManagement
    | [<EndPoint "/orgmaintenance">] OrgPositionMaintenance
    | [<EndPoint "/setup">] CompanySetup

module Templating =
    open WebSharper.UI.Next.Html

    type MainTemplate = Templating.Template<"Main.html">
    


    let a'attr'1 =  Attr.Create "href" "#"
    let a'attr'2 = Attr.Create "class" "dropdown-toggle"
    let a'attr'3 = Attr.Create "data-toggle" "dropdown"
    let a'attr'4 = Attr.Create "role" "button"
    let a'attr'5 = Attr.Create "aria-haspopup" "true"
    let a'attr'6 = Attr.Create "aria-expanded" "false"

    
    let a'attr'list = [ a'attr'1; a'attr'2; a'attr'3; a'attr'4; a'attr'5; a'attr'6] :> seq<Attr>

    let li'sep'attr'1 = Attr.Create "role" "separator"
    let li'sep'attr'2 = Attr.Create "class" "divider"
    let li''sep'attr'list = [ li'sep'attr'1; li'sep'attr'2] :> seq<Attr>


    let v'menu = Var.Create("")
    let doc p =
        li[
            aAttr[Attr.Create "href" "setup"][Doc.TextNode p]
        ]    



    let loaddata =
        v'menu.View
        |> View.MapAsync( fun s -> async{
            let!  data = Server.GetMainMenu(0, "WEBSITE")

            //JavaScript.Console.Log ("trying to get menu from server " + data.Head)
            
            return Doc.Empty
        }
        )
    
    
    // Compute a menubar where the menu item for the given endpoint is active
    let MenuBar (ctx: Context<EndPoint>) endpoint : Doc list =




        let myTree =
            { menucode = "0"; Description = "Root"; Children = 
            [
                { menucode = "1"; Description = "Mechanical"; Children = [] };
                { menucode = "2"; Description = "Electrical"; Children =         
                [
                    { menucode = "3"; Description = "High Voltage"; Children = 
                    [
                        { menucode = "5"; Description = "HV Maintanence"; Children = [] };
                        { menucode = "6"; Description = "City Power"; Children = [] }
                    ] };
                    { menucode = "4"; Description = "Low Voltage"; Children = 
                    [
                        { menucode = "7"; Description = "LV Wiring"; Children = [] } ;
                        { menucode = "8"; Description = "LV Maintanence"; Children = [] }
                    ] }
                ]};
            ]}

        //let rec getChildren (node : Node) = 
        //    Seq.concat [node.Children; (Seq.collect getChildren node.Children)]

        let mutable rootNode = { menucode = "0"; Description = "Root"; Children = [] }

        let menu0 :T5PMenu= {english="Root"; menulevel=0; menucode="ROOTNODE"; submenu="WEBSITE"}
        let menu1 :T5PMenu= {english="System Configuration"; menulevel=1; menucode="WEBSITE"; submenu="SYSCFG"}
        let menu11 :T5PMenu= {english="Company Information Setup"; menulevel=2; menucode="SYSCFG"; submenu=""}
        let menu2 :T5PMenu= {english="System Utility"; menulevel=1; menucode="WEBSITE"; submenu="SYSUTY"}

        let theT5pMenu = [ 
            menu0;
            menu1;
            menu11;
            menu2
            ]

        let ListMenuToT5PMenu (data:string*string*string*int) =
            let menucode, english, submenu, menulevel = data
            let menu :T5PMenu= {english=english; menulevel=menulevel; menucode=menucode; submenu=submenu}
            menu

        let  allData = Server.GetAllMenu() 
        let runAllData = allData |> Async.RunSynchronously

        let getChildren( fullList:T5PMenu list, submenu) =
            fullList
            |> List.filter (fun c -> c.menucode = submenu)

        let mappedMenu =  List.map ListMenuToT5PMenu runAllData


        //let a = getChildren(theT5pMenu, "SYSCFG")
        let rec toTree(root:T5PMenu) =
          {            
            menucode = root.menucode
            Description = root.english
            Children  = 
                getChildren(mappedMenu, root.submenu) 
                |> List.map toTree
          }

        let theMainNode = toTree(menu0);
        let rec addTopMenu (p:Node) = 
            if Seq.length( p.Children ) = 0 then
                li[
                    aAttr[Attr.Create "href" "setup"][Doc.TextNode p.Description]
                  ]
            else
                li[
                //liAttr [attr.``class`` "dropdown"] [
                //    aAttr a'attr'list [
                //        Doc.TextNode (p.Description + " ")
                //        spanAttr[Attr.Create "class" "caret"][Doc.TextNode ""]
                //    ]
                    aAttr[Attr.Create "href" "#"][
                        Doc.TextNode (p.Description + " ")
                        spanAttr[attr.``class`` "caret"][
                            ]
                        ]
                    ulAttr[attr.``class`` "dropdown-menu"][
                        //p.Children
                        //|> Seq.map addTopMenu
                        let docMenu2 = p.Children |> Seq.map addTopMenu 
                        for x in docMenu2 do
                            yield x :> Doc
                    ]
                ]

        //Get all Root nodes
        let listMainNodes = List.ofSeq theMainNode.Children
            
        //Convert toot nodes to HTML
        let buildTopMenu =
            List.map addTopMenu listMainNodes

//        let  data = Server.GetMainMenu(0, "WEBSITE") 
//        
//        let  dataNext menucode = Server.GetMainMenu(1, menucode) 
//
//        let functiontomap (data:int*string*string*string*string) =
//            let menuorder, english, chinese, menucode, submenu = data
//            let menu :Menu= {menuorder=1;english=english; chinese=chinese; menucode=menucode; submenu=submenu}
//            menu
//
//        let menuNextMap (data:int*string*string*string*string) =
//            let menuorder, english, chinese, menucode, submenu = data
//            let menu :Menu= {menuorder=1;english=english; chinese=chinese; menucode=menucode; submenu=submenu}
//            menu
//        
//        let menuNextRun(menucode: string) = dataNext(menucode) |> Async.RunSynchronously
//        let menuNextConvert(menucode:string) =  menuNextRun(menucode) |> List.map functiontomap 
//
//        let r = data |> Async.RunSynchronously
//        let r1 =  List.map functiontomap r
//
//        let addMenu (p:Menu) =
//            if p.english.Length > 0 then
//                li[
//                    aAttr[Attr.Create "href" "setup"][Doc.TextNode p.english]
//                  ]
//            else
//                liAttr li''sep'attr'list [Doc.TextNode ""]
//
//
//        let docMenu1 = menuNextConvert("SYSCFG") |> List.map addMenu 
//        let mainTopMenu = List.map addMenu r1

//        let addTopMenu1 (p:Menu) = 
//            liAttr [attr.``class`` "dropdown"] [
//                aAttr a'attr'list [
//                    Doc.TextNode (p.english + " ")
//                    spanAttr[Attr.Create "class" "caret"][Doc.TextNode ""]
//                ]
//                ulAttr[attr.``class`` "dropdown-menu"][
//                    let docMenu2 = menuNextConvert(p.submenu) |> List.map addMenu 
//                    for x in docMenu2 do
//                        yield x :> Doc
//                ]
//            ]
        

//        let buildTopMenu1 =
//            List.map addTopMenu r1





        let ( => ) txt act =
             liAttr [if endpoint = act then yield attr.``class`` "active"] [
                aAttr [attr.href (ctx.Link act)] [text txt]
             ]
        [
            //li ["Home" => EndPoint.Home]
            //li ["About" => EndPoint.About]

            for x in buildTopMenu do
                yield x :> Doc
            //addTopMenu (r1.Head):> Doc 

//            liAttr [attr.``class`` "dropdown"] [
//                aAttr a'attr'list [
//                    Doc.TextNode "System Configuration "
//                    spanAttr[Attr.Create "class" "caret"][Doc.TextNode ""]
//                ]
//                ulAttr[attr.``class`` "dropdown-menu"][
//                    for x in docMenu1 do
//                        yield x :> Doc
//                ]
//                    
//            ]
            
//            
//            liAttr [attr.``class`` "dropdown"] [
//                aAttr a'attr'list [
//                    Doc.TextNode "System Configuration "
//                    spanAttr[Attr.Create "class" "caret"][Doc.TextNode ""]
//                ]
//                ulAttr[attr.``class`` "dropdown-menu"][
//                    li[
//                        aAttr[Attr.Create "href" "setup"][Doc.TextNode "Company Information Setup"]
//                    ]
//                    li[
//                        aAttr[Attr.Create "href" "orgmaintenance"][Doc.TextNode "Organization & Position Maintenance"]
//                    ]
//                    li[
//                        aAttr[Attr.Create "href" "#"][Doc.TextNode "Company Management"]
//                    ]
//                    liAttr li''sep'attr'list [Doc.TextNode ""]      
//                    li[
//                        aAttr[Attr.Create "href" "#"][Doc.TextNode "Company Management"]
//                    ]                                      
//                ]
//            ]
//            liAttr [attr.``class`` "dropdown"] [
//                aAttr a'attr'list [
//                    Doc.TextNode "System Utility "
//                    spanAttr[Attr.Create "class" "caret"][Doc.TextNode ""]
//                ]
//                ulAttr[attr.``class`` "dropdown-menu"][
//                    li[
//                        aAttr[Attr.Create "href" "#"][Doc.TextNode "Interface report"]
//                    ]
//                    li[
//                        aAttr[Attr.Create "href" "process"][Doc.TextNode "Process Schedule Maintenance"]
//                    ]
//                    liAttr li''sep'attr'list [Doc.TextNode ""]
//                    li[
//                        aAttr[Attr.Create "href" "#"][Doc.TextNode "Calendar Management"]
//                    ]                                      
//                ]
//            ]      
        ]

    let Main ctx action title body =
        Content.Page(
            MainTemplate.Doc(
                title = title,
                menubar = MenuBar ctx action,
                body = body
            )
        )

module Site =
    open WebSharper.UI.Next.Html

    let HomePage ctx =
        Templating.Main ctx EndPoint.Home "Home" [
            h1 [text "Say Hi to the server!"]

            h1 [text "Say Hi to the server!"]
            h1 [text "Say Hi to the server!"]            
            //div [client <@ Client.Main() @>]
        ]

    let AboutPage ctx =
        Templating.Main ctx EndPoint.About "About" [
            h1 [text "About"]
            p [text "This is a template WebSharper client-server application."]
        ]

    let ProcessManagementPage ctx =
        Templating.Main ctx EndPoint.ProcessManagement "Process Management" [
            h1 [text "Process Management"]
            div [client <@ Client.Main() @>]
        ]
    let CompanySetupPage ctx =
        Templating.Main ctx EndPoint.ProcessManagement "Company Setup" [
            h1 [text "Company Setup"]
            p [text "This is a template WebSharper client-server application."]
        ]
    let OrgPositionMaintenancePage ctx =
        Templating.Main ctx EndPoint.ProcessManagement "Organization & Position Maintenance" [
            h1 [text "Organization & Position Maintenance"]
            p [text "This is a template WebSharper client-server application."]
        ]


    [<Website>]
    let Main =




        //Application.Text(fun ctx -> s)
            
        

        Application.MultiPage (fun ctx endpoint ->
            match endpoint with
            | EndPoint.Home -> HomePage ctx
            | EndPoint.About -> AboutPage ctx
            | EndPoint.ProcessManagement -> ProcessManagementPage ctx
            | EndPoint.OrgPositionMaintenance -> OrgPositionMaintenancePage ctx
            | EndPoint.CompanySetup -> CompanySetupPage ctx
        )
