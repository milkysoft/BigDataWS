namespace Application2

open WebSharper
open FSharp.Data
open System
open System.Data.SqlClient

module Database =
    module Sql = 
        [<Literal>]
        
        let connectionString = 
            """Data Source=.;Initial Catalog=BigData;User Id=zuoqin;Password=Qwerty123;MultipleActiveResultSets=True;"""
        type GetMainMenu =
            SqlCommandProvider<"SELECT a.menuorder, a.english, a.chinese, a.menucode, a.submenu FROM menus a with (nolock) WHERE a.menucode<>'FAVORITE' AND a.submenu<>'FAVORITE' AND a.menucode<>'ESS' AND a.submenu<>'ESS' and menulevel = @level and menucode=@menucode and (a.menucode + '_' +convert(nvarchar, a.menuopt) <> 'PAYROLL_9' )  ORDER BY a.menulevel,a.menucode,CASE WHEN a.menuopt<1000 AND a.menucode='WEBSITE' THEN 0-a.menuorder ELSE a.menuorder END,a.moduletype" , connectionString, ResultType = ResultType.Tuples>

        type GetAllPost  = 
            SqlCommandProvider<""" SELECT Id, CreateDate, EditDate,
            Content, Title FROM Post""", connectionString, ResultType = ResultType.Tuples>

        type SetPost = SqlCommandProvider<"""
            UPDATE Post
            SET EditDate = GETDATE(), Content = @content, Title = @title    
            WHERE Id=@id""" , connectionString>

        type GetAllMenu =
            SqlCommandProvider<"

SELECT a.menucode, a.english, a.submenu, a.menulevel FROM menus a with (nolock) WHERE a.menucode<>'FAVORITE' AND a.submenu<>'FAVORITE'
AND a.menucode<>'ESS' AND a.submenu<>'ESS'  
and (a.menucode + '_' +convert(nvarchar, a.menuopt) <> 'PAYROLL_9' )  
ORDER BY a.menulevel,a.menucode,
CASE WHEN a.menuopt<1000 AND a.menucode='WEBSITE'
    THEN 0-a.menuorder
    ELSE a.menuorder
END, a.moduletype" , connectionString, ResultType = ResultType.Tuples>


        let conn = 
            let conn = new SqlConnection(connectionString)
            conn.Open()
            conn


    let getMainMenu(levelparam, menucodeparam) = 
        (new Sql.GetMainMenu()).Execute(level = levelparam, menucode = menucodeparam)
        //(new Sql.GetMainMenu()).AsyncExecute() 

    let getAllMenu () = 
        (new Sql.GetAllMenu()).Execute()


    let getAllPosts () = 
        (new Sql.GetAllPost()).Execute()


    let setPost1 id title content =
        (new Sql.SetPost()).AsyncExecute(id = id, title = title, content = content ) 