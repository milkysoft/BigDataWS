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
