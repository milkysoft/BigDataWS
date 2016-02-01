namespace Application2

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI.Next
open WebSharper.UI.Next.Client
open WebSharper.UI.Next.Html

[<JavaScript>]
module Client =

    open WebSharper.JavaScript
    open WebSharper.UI.Next.Client
    open WebSharper.Forms
    open WebSharper.Forms.Bootstrap.Controls
    let cls = Attr.Class
    
    let rvUsername = Var.Create ""


    let LoginForm () =
        JavaScript.Console.Log "Trying to create LoginForm"
        Form.Return (fun user pass check -> user, pass, check)
        <*> (Form.Yield ""
            |> Validation.IsNotEmpty "Must enter a username")
        <*> (Form.Yield ""
            |> Validation.IsNotEmpty "Must enter a password")
        <*> Form.Yield false
        |> Form.WithSubmit
        |> Form.Run (fun (user, pass, check) ->
            JS.Alert("Welcome, " + user + "!")
        )
        |> Form.Render (fun user pass check submit ->
            form [
                Input "Username" [] (rvUsername, [cls "sr-only"], [cls "input-lg"; attr.readonly ""])
                Simple.TextAreaWithError "Username - echoed" rvUsername submit.View
                TextAreaWithError "Username - echoed" [] (rvUsername, [], []) submit.View
                Simple.InputWithError "Username - echoed" rvUsername submit.View
                Simple.InputPasswordWithError "Password" pass submit.View
                Checkbox "Keep me logged in" [] (check, [cls "input-lg"], [])
                Radio "This is a radio button" [] (check, [], [])
                Button "Log in" [Class "btn btn-primary"] submit.Trigger
                ShowErrors [attr.style "margin-top:1em;"] submit.View
            ])

    // Story model
    type Menu =
        {   menuorder : int            
            english : string
            chinese : string
            menucode : string
    }

    type MenuModel = 
        {   Posts : ListModel<int,MenuModel> }

    type Post = 
        {   Id : int
            Num : int
            Title : string
            Content : string
            CreateDate : string
            EditDate : string }

    // Story model
    type PostModel =
        {   Id : int            
            Title : Var<string>
            Content : Var<string>
            CreateDate : string
            EditDate : Var<string> 
            Num : Var<int>
            EditedTitle : Var<string>
            EditedContent : Var<string>
            IsEditMode : Var<bool>
    }

    // Blog model
    type BlogModel = 
        {   Posts : ListModel<int,PostModel> }
    

    let post1 =
         {Id=1; Title=Var.Create "111ssdfds"; Content = Var.Create "333fsfsdf";CreateDate= "2015-01-01";EditDate=Var.Create "2015-01-01";Num=Var.Create 1;EditedTitle=Var.Create "dsadsdsa"; EditedContent = Var.Create "dsadsad"; IsEditMode=Var.Create false}

    let post2 =
         {Id=2; Title=Var.Create "222ssdfds"; Content = Var.Create "44444fsfsdf";CreateDate= "2015-01-01";EditDate=Var.Create "2015-01-01";Num=Var.Create 1;EditedTitle=Var.Create "dsadsdsa"; EditedContent = Var.Create "dsadsad"; IsEditMode=Var.Create false}
    

    let postList = ListModel.Create (fun i ->  int i.Id) []


    let rvSubmit = Var.Create ()
    
    let v'blog =
        View.Do{
            return 1 }

        |> View.MapAsync ( fun i -> async{            
            return postList
            }  )

    let doc'header (post : PostModel) = 
        divAttr[Attr.Class "panel-heading"][
            div[ Doc.TextView post.Title.View ]
        ]

//    let editForm =
//            divAttr attrs_div [
//                v'blog  
//                |> View.Map ( fun list -> 
//                    ListModel.View blog.Posts
//                    |> Doc.BindSeqCached (fun p -> 
//                        doc p
//                    )
//                )
//                |> Doc.EmbedView
//            ]
    
    let doc'body (post : PostModel) = 
        divAttr[Attr.Class "panel-body"][
            div[ Doc.TextView post.Content.View ]
            p[Doc.TextView post.EditDate.View]

            Doc.Button
            <| "New Item"
            <| [Attr.Class "btn btn-default"]
            <| fun _ ->
                rvUsername.Value <- post.Content.Value
                JQuery.JQuery.Of("#blogItems").Hide().Ignore
                JQuery.JQuery.Of("#loginform").RemoveClass("divhidden").AddClass("divshown").Ignore
                JavaScript.Console.Log 1
        ]

    let doc post  =
        divAttr [Attr.Class "panel panel-primary"; Attr.Create "id" ( sprintf "post-%d-article" post.Id)][
            doc'header post
            doc'body post
            ]

//      this working
//    let loaddata =
//        rvUsername.View
//        |> View.MapAsync( fun s -> async{
//            JavaScript.Console.Log ("Successfull login, var'is'logged'in: ")
//            return Doc.Empty
//        }
//        )

    let v'menu = Var.Create("")
    let loaddata =
        v'menu.View
        |> View.MapAsync( fun s -> async{
            let!  data = Server.GetMainMenu(0, "WEBSITE")
            let menuorder, english, chinese, menucode, submenu = data.Head

            let theModel  =  data.Head
            JavaScript.Console.Log ("trying to get menu from server " + english)
            
            return Doc.Empty
        }
        )

    let mutable iid = 5
    let addNewBlog =
        iid <- iid + 1
        postList.Add({Id=iid; Title=Var.Create( "New item " + iid.ToString() + " title"); Content = Var.Create "44444fsfsdf";CreateDate= "2015-01-01";EditDate=Var.Create "2015-01-01";Num=Var.Create 1;EditedTitle=Var.Create "dsadsdsa"; EditedContent = Var.Create "dsadsad"; IsEditMode=Var.Create false})
        JavaScript.Console.Log iid


    let doc'main'menu =
        ulAttr [Attr.Class "nav navbar-nav"][
            liAttr [Attr.Class "dropdown open"] [
                aAttr[Attr.Class "dropdown-toggle"][
                    Doc.TextNode "System Configuration"
                ]
                ulAttr [Attr.Class "dropdown-menu"][

                    li [Doc.TextNode "System Configuration"]
                    li [Doc.TextNode "System Configuration"]
                    li [Doc.TextNode "System Configuration"]
                ]
            ]
            liAttr [Attr.Class "dropdown open"] [
                aAttr[Attr.Class "dropdown-toggle"][
                    Doc.TextNode "System Utility"
                ]
                ulAttr [Attr.Class "dropdown-menu"][

                    li [Doc.TextNode "Interface report"]
                    li [Doc.TextNode "Process Schedule Maintenance"]
                    li [Doc.TextNode "Calendar Management"]
                ]
            ]
            li [Doc.TextNode "System Utility"]
            li [Doc.TextNode "System Maintenance"]
            li [Doc.TextNode "Payroll Process Management"]
            li [Doc.TextNode "Human Resource Management"]

        ]

    let doc'nav'left =
            div[Doc.TextNode "jkhkjhjk"]
            //divAttr [Attr.Class "navbar-collapse collapse"][
                
                //doc'main'menu
                    
//                   li [ Doc.Button
//                        <| "New Item"
//                        <| []
//                        <| fun _ ->
//                            iid <- iid + 1
//                            postList.Add({Id=iid; Title=Var.Create( "New item " + iid.ToString() + " title"); Content = Var.Create "44444fsfsdf";CreateDate= "2015-01-01";EditDate=Var.Create "2015-01-01";Num=Var.Create 1;EditedTitle=Var.Create "dsadsdsa"; EditedContent = Var.Create "dsadsad"; IsEditMode=Var.Create false})
//                            JavaScript.Console.Log iid
//                     ]
//
//                   li [ Doc.Button
//                        <| "Delete Item"
//                        <| []
//                        <| fun _ ->                            
//                            postList.RemoveByKey iid
//                            iid <- iid - 1
//                            JavaScript.Console.Log iid
//                     ]


            //]

    let Main () =
        postList.Add post1
        postList.Add post2
        let blog = 
            { Posts = postList }
        let rvInput = Var.Create ""
        let submit = Submitter.CreateOption rvInput.View
        let vReversed =
            submit.View.MapAsync(function
                | None -> async { return "" }
                | Some input -> Server.DoSomething input
            )

        
        
        
           


//        div [
//            Doc.Input [] rvInput
//            Doc.Button "Send" [] submit.Trigger
//            hr []
//            h4Attr [attr.``class`` "text-muted"] [text "The server responded:"]
//            divAttr [attr.``class`` "jumbotron"] [h1 [textView vReversed]]
//        ]


        let attr'my1 =  Attr.Create "placeholder" "Search"
        let attr'my2 = Attr.Class "mainsearch"
        let attrs = Seq.append [| attr'my1|] [ attr'my2]

        let attr'divid =  Attr.Create "id" "blogItems"
        let attr'divclass = Attr.Class "col-md-12"
        let attrs_div = Seq.append [|attr'divid|] [ attr'divclass]

        let attr'login'divid =  Attr.Create "id" "loginform"
        let attr'login'divclass = Attr.Class "divhidden"
        let attrs_login'div = Seq.append [|attr'login'divid|] [ attr'login'divclass]
        let attr'login'style = Attr.Style "visibility" "hidden"
        let attrs_login'style'seq = Seq.append [attr'login'divclass] [attr'login'divid]
        // stories page
        divAttr[Attr.Class "container"][
//            divAttr attrs_login'style'seq [
//                LoginForm()
//            ]
            //LoginUI.zor |> Doc.EmbedView 
            divAttr [Attr.Class "navbar navbar-default navbar-fixed-top"][
                //divAttr [Attr.Class "navbar-header"][
                doc'nav'left
                //]
                divAttr [Attr.Class "navbar-collapse collapse"][
                    divAttr [Attr.Class "navbar-collapse collapse"][
                        divAttr [Attr.Class "pull-right"][
                            divAttr [Attr.Class "nav"][
                                ulAttr[Attr.Class "nav navbar-nav sm sm-collapsible"][
                                    li[
                                        Doc.Input attrs rvUsername                            
                                    ]
                                ]

                                loaddata |> Doc.EmbedView

                                

                            ]
                        ]
                    ]
                ]
            ]
            divAttr attrs_div [
                v'blog  
                |> View.Map ( fun list -> 
                    ListModel.View blog.Posts
                    |> Doc.BindSeqCached (fun p -> 
                        doc p
                    )
                )
                |> Doc.EmbedView
            ]
        ]
