namespace Application2

open WebSharper
open FSharp.Data
open System
open System.Data.SqlClient

module Database =
    module Sql = 
        [<Literal>]
        
        let connectionString = 
            """Data Source=.;Initial Catalog=rtest;User Id=zuoqin;Password=Qwerty123;MultipleActiveResultSets=True;"""
        type GetMainMenu =
            SqlCommandProvider<"SELECT a.menuorder, a.english, a.chinese, a.menucode, a.submenu FROM menus a with (nolock) WHERE a.menucode<>'FAVORITE' AND a.submenu<>'FAVORITE' AND a.menucode<>'ESS' AND a.submenu<>'ESS' and menulevel = @level and menucode=@menucode and (a.menucode + '_' +convert(nvarchar, a.menuopt) <> 'PAYROLL_9' )  ORDER BY a.menulevel,a.menucode,CASE WHEN a.menuopt<1000 AND a.menucode='WEBSITE' THEN 0-a.menuorder ELSE a.menuorder END,a.moduletype" , connectionString, ResultType = ResultType.Tuples>

        let conn = 
            let conn = new SqlConnection(connectionString)
            conn.Open()
            conn


    let getMainMenu(levelparam, menucodeparam) = 
        (new Sql.GetMainMenu()).Execute(level = levelparam, menucode = menucodeparam)
        //(new Sql.GetMainMenu()).AsyncExecute() 
