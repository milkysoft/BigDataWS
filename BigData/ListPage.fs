namespace Application2

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI.Next
open WebSharper.UI.Next.Client
open WebSharper.UI.Next.Html

[<JavaScript>]
module ListPage =

    open WebSharper.JavaScript
    open WebSharper.UI.Next.Client
    open WebSharper.Forms
    open WebSharper.Forms.Bootstrap.Controls
    let cls = Attr.Class
    
    let rvSearch = Var.Create ""

    let rvPostContent = Var.Create ""
    let rvPostTitle = Var.Create ""

    let rvRowIndex = Var.Create 0

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
        let attr'container1 =  Attr.Create "class" "container"
        let attr'container2 = Attr.Style "margin-top" "100px"
        let attrs'container = Seq.append [| attr'container1|] [ attr'container2]

        let attr'title1 =  Attr.Create "placeholder" "Title"
        let attr'title2 = Attr.Class "form-control"
        let attrs'title = Seq.append [| attr'title1|] [ attr'title2]


        divAttr[Attr.Class "panel-body" ][
            div[ Doc.TextView post.Content.View ]
            p[Doc.TextView post.EditDate.View]

            Doc.Button
            <| "New Item"
            <| [Attr.Class "btn btn-default"]
            <| fun _ ->
                rvPostContent.Value <- post.Content.Value
                rvPostTitle.Value <- post.Title.Value
                rvRowIndex.Value <- post.Id
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
            JavaScript.Console.Log ("trying to get menu from server inside ListPage" + english)
            
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

        let attr'container1 =  Attr.Create "class" "container"
        let attr'container2 = Attr.Style "margin-top" "100px"
        let attrs'container = Seq.append [| attr'container1|] [ attr'container2]

        let attr'title1 =  Attr.Create "placeholder" "Title"
        let attr'title2 = Attr.Class "form-control"
        let attrs'title = Seq.append [| attr'title1|] [ attr'title2]

        let attr'content1 =  Attr.Create "placeholder" "Content"
        let attr'content2 = Attr.Class "form-control"
        let attrs'content = Seq.append [| attr'content1|] [ attr'content2]

        let attr'search1 =  Attr.Create "placeholder" "Search"
        let attr'search2 = Attr.Class "mainsearch"
        let attrs'search = Seq.append [| attr'search1|] [ attr'search2]

        let attr'divid =  Attr.Create "id" "blogItems"
        let attr'divclass = Attr.Class "col-md-12"
        let attrs_div = Seq.append [|attr'divid|] [ attr'divclass]

        let attr'login'divid =  Attr.Create "id" "loginform"
        let attr'login'divclass = Attr.Class "divhidden"
        let attrs_login'div = Seq.append [|attr'login'divid|] [ attr'login'divclass]
        let attr'login'style = Attr.Style "display" "none"
        let attrs_login'style'seq = Seq.append [attr'login'divclass] [attr'login'divid]


        let replacePost (post : PostModel) : PostModel option =
            let newPost = {Id=post.Id; Title=Var.Create rvPostTitle.Value;
                            Content = Var.Create rvPostContent.Value;
                            CreateDate= "2015-01-01";EditDate=Var.Create "2015-01-01";
                            Num=Var.Create 1;EditedTitle=Var.Create "dsadsdsa";
                            EditedContent = Var.Create "dsadsad"; IsEditMode=Var.Create false}

            
            if newPost.Id > 0 then Some(newPost) else None

            
        // stories page
        divAttr attrs'container [
            divAttr attrs_login'style'seq [
                divAttr [attr.``class`` "form-group"][
                    divAttr[attr.``class`` "col-sm-12"][
                        label [ Doc.TextNode "Title"]
                    ]
                    divAttr[attr.``class`` "col-sm-12"][
                        Doc.InputArea attrs'title rvPostTitle
                    ]
                    divAttr[attr.``class`` "col-sm-12"][
                        label [ Doc.TextNode "Content"]
                    ]
                    divAttr[attr.``class`` "col-sm-12"][
                        Doc.InputArea attrs'title rvPostContent
                    ]
                    Doc.Button
                    <| "Save"
                    <| [Attr.Class "btn btn-default"]
                    <| fun _ ->
                        JQuery.JQuery.Of("#blogItems").Show().Ignore
                        JQuery.JQuery.Of("#loginform").RemoveClass("divshown").AddClass("divhidden").Ignore

                        let a = JQuery.JQuery.Of(sprintf "#post-%d-article" rvRowIndex.Value).Children(".panel-heading").First().Children().First()
                        let post = postList.FindByKey(rvRowIndex.Value)
                        postList.UpdateBy replacePost rvRowIndex.Value
                        //JavaScript.Console.Log( rvPostTitle.Value )
                        //JavaScript.Console.Log( rvPostContent.Value )
                        //JQuery.JQuery.Of(sprintf "#post-%d-article" rvRowIndex.Value).Children(".panel-heading").First().Children().First().Text(rvPostTitle.Value).Ignore
                        
                        //JQuery.JQuery.Of(sprintf "#post-%d-article" rvRowIndex.Value).Children(".panel-body").First().Children().First().Text(rvPostContent.Value).Ignore


                        //JavaScript.Console.Log( a.Text() )

                    Doc.Button
                    <| "Cancel"
                    <| [Attr.Class "btn btn-default"]
                    <| fun _ ->
                        JQuery.JQuery.Of("#blogItems").Show().Ignore
                        JQuery.JQuery.Of("#loginform").RemoveClass("divshown").AddClass("divhidden").Ignore
                        JavaScript.Console.Log 1
                ]
            ]
            divAttr [Attr.Class "navbar navbar-default navbar-fixed-top"][
                doc'nav'left
                divAttr [Attr.Class "navbar-collapse collapse"][
                    divAttr [Attr.Class "navbar-collapse collapse"][
                        divAttr [Attr.Class "pull-right"][
                            divAttr [Attr.Class "nav"][
                                ulAttr[Attr.Class "nav navbar-nav sm sm-collapsible"][
                                    li[
                                        Doc.Input attrs'search rvSearch
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
