namespace Application2

open WebSharper

module Server =

    [<Rpc>]
    let DoSomething input =
        let R (s: string) = System.String(Array.rev(s.ToCharArray()))
        async {
            return R input
        }

    [<Rpc>]
    let GetMainMenu level =
        let mutable s = Database.getMainMenu(level)
                        |> List.ofSeq
                        //|> String.concat ""
        
        async {            
            return s
        }
    
    [<Rpc>]
    let GetAllMenu() =
        let mutable s = Database.getAllMenu()
                        |> List.ofSeq
        
        async {            
            return s
        }

    [<Rpc>]
    let GetAllPosts() =
        let mutable s = Database.getAllPosts()
                        |> List.ofSeq 
        
        async {            
            return s
        }

    [<Rpc>]
    let setPostContent id title content  = 
       Database.setPost1 id title content
