(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,console,alert,Forms,Pervasives,Form1,Form,Validation,List,Bootstrap,Controls,T,Application2,Client,UI,Next,AttrProxy,Simple,Doc,Var,Submitter,Remoting,AjaxRemotingProvider,Concurrency,View,AttrModule,Seq,ListModel1,String,Var1,jQuery,ListModel,View1;
 Runtime.Define(Global,{
  Application2:{
   Client:{
    LoginForm:function()
    {
     var _arg00_,_arg00_1,_arg10_,_arg10_1,_arg10_2,x,renderFunction;
     if(console)
      {
       console.log("Trying to create LoginForm");
      }
     _arg00_=function(tupledArg)
     {
      var user;
      user=tupledArg[0];
      tupledArg[1];
      tupledArg[2];
      return alert("Welcome, "+user+"!");
     };
     _arg10_=Form.Yield("");
     _arg10_1=Form.Yield("");
     _arg00_1=Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Form1.Return(function(user)
     {
      return function(pass)
      {
       return function(check)
       {
        return[user,pass,check];
       };
      };
     }),Validation.IsNotEmpty("Must enter a username",_arg10_)),Validation.IsNotEmpty("Must enter a password",_arg10_1)),Form.Yield(false));
     _arg10_2=Form.WithSubmit(_arg00_1);
     x=Form.Run(_arg00_,_arg10_2);
     renderFunction=function()
     {
      return function(pass)
      {
       return function(check)
       {
        return function(submit)
        {
         var arg20,clo1,arg10,clo2,tupledArg,arg201,arg21,arg22;
         clo1=Controls.TextAreaWithError("Username - echoed");
         arg10=Runtime.New(T,{
          $:0
         });
         clo2=clo1(arg10);
         tupledArg=[Client.rvUsername(),Runtime.New(T,{
          $:0
         }),Runtime.New(T,{
          $:0
         })];
         arg201=tupledArg[0];
         arg21=tupledArg[1];
         arg22=tupledArg[2];
         arg20=List.ofArray([Controls.Input("Username",Runtime.New(T,{
          $:0
         }),Client.rvUsername(),List.ofArray([(Client.cls())("sr-only")]),List.ofArray([(Client.cls())("input-lg"),AttrProxy.Create("readonly","")])),Simple.TextAreaWithError("Username - echoed",Client.rvUsername(),submit.get_View()),(clo2([arg201,arg21,arg22]))(submit.get_View()),Simple.InputWithError("Username - echoed",Client.rvUsername(),submit.get_View()),Simple.InputPasswordWithError("Password",pass,submit.get_View()),Controls.Checkbox("Keep me logged in",Runtime.New(T,{
          $:0
         }),check,List.ofArray([(Client.cls())("input-lg")]),Runtime.New(T,{
          $:0
         })),Controls.Radio("This is a radio button",Runtime.New(T,{
          $:0
         }),check,Runtime.New(T,{
          $:0
         }),Runtime.New(T,{
          $:0
         })),(((Controls.Button())("Log in"))(List.ofArray([(Controls.Class())("btn btn-primary")])))(function()
         {
          return submit.Trigger();
         }),Controls.ShowErrors(List.ofArray([AttrProxy.Create("style","margin-top:1em;")]),submit.get_View())]);
         return Doc.Element("form",[],arg20);
        };
       };
      };
     };
     return Form1.Render(renderFunction,x);
    },
    Main:function()
    {
     var blog,rvInput,submit,arg00,arg10,vReversed,_attr_my1,_attr_my2,attrs,_attr_divid,_attr_divclass,attrs_div,_attr_login_divid,_attr_login_divclass,_attrs_login_div,_attr_login_style,_attrs_login_style_seq,ats,ats1,ats2,ats3,ats4,arg20;
     Client.postList().Add(Client.post1());
     Client.postList().Add(Client.post2());
     blog={
      Posts:Client.postList()
     };
     rvInput=Var.Create("");
     submit=Submitter.CreateOption(rvInput.get_View());
     arg00=function(_arg1)
     {
      return _arg1.$==1?AjaxRemotingProvider.Async("Application2:0",[_arg1.$0]):Concurrency.Delay(function()
      {
       return Concurrency.Return("");
      });
     };
     arg10=submit.get_View();
     vReversed=View.MapAsync(arg00,arg10);
     _attr_my1=AttrProxy.Create("placeholder","Search");
     _attr_my2=AttrModule.Class("mainsearch");
     attrs=Seq.append([_attr_my1],List.ofArray([_attr_my2]));
     _attr_divid=AttrProxy.Create("id","blogItems");
     _attr_divclass=AttrModule.Class("col-md-12");
     attrs_div=Seq.append([_attr_divid],List.ofArray([_attr_divclass]));
     _attr_login_divid=AttrProxy.Create("id","loginform");
     _attr_login_divclass=AttrModule.Class("divhidden");
     _attrs_login_div=Seq.append([_attr_login_divid],List.ofArray([_attr_login_divclass]));
     _attr_login_style=AttrModule.Style("visibility","hidden");
     _attrs_login_style_seq=Seq.append(List.ofArray([_attr_login_divclass]),List.ofArray([_attr_login_divid]));
     ats=List.ofArray([AttrModule.Class("container")]);
     ats1=List.ofArray([AttrModule.Class("navbar navbar-default navbar-fixed-top")]);
     ats2=List.ofArray([AttrModule.Class("navbar-collapse collapse")]);
     ats3=List.ofArray([AttrModule.Class("navbar-collapse collapse")]);
     ats4=List.ofArray([AttrModule.Class("pull-right")]);
     arg20=List.ofArray([Doc.Input(attrs,Client.rvUsername())]);
     return Doc.Element("div",ats,List.ofArray([Doc.Element("div",ats1,List.ofArray([Client["doc'nav'left"](),Doc.Element("div",ats2,List.ofArray([Doc.Element("div",ats3,List.ofArray([Doc.Element("div",ats4,List.ofArray([Doc.Element("div",List.ofArray([AttrModule.Class("nav")]),List.ofArray([Doc.Element("ul",List.ofArray([AttrModule.Class("nav navbar-nav sm sm-collapsible")]),List.ofArray([Doc.Element("li",[],arg20)])),Doc.EmbedView(Client.loaddata())]))]))]))]))])),Doc.Element("div",attrs_div,List.ofArray([Doc.EmbedView(View.Map(function()
     {
      return Doc.Convert(function(p)
      {
       return Client.doc(p);
      },ListModel1.View(blog.Posts));
     },Client["v'blog"]()))]))]));
    },
    addNewBlog:Runtime.Field(function()
    {
     var _,a;
     _=Client.iid()+1;
     Client.iid=function()
     {
      return _;
     };
     Client.postList().Add({
      Id:Client.iid(),
      Title:Var.Create("New item "+String(Client.iid())+" title"),
      Content:Var.Create("44444fsfsdf"),
      CreateDate:"2015-01-01",
      EditDate:Var.Create("2015-01-01"),
      Num:Var.Create(1),
      EditedTitle:Var.Create("dsadsdsa"),
      EditedContent:Var.Create("dsadsad"),
      IsEditMode:Var.Create(false)
     });
     a=Client.iid();
     return console?console.log(a):undefined;
    }),
    cls:Runtime.Field(function()
    {
     return function(name)
     {
      return AttrModule.Class(name);
     };
    }),
    doc:function(post)
    {
     var arg10;
     arg10="post-"+Global.String(post.Id)+"-article";
     return Doc.Element("div",List.ofArray([AttrModule.Class("panel panel-primary"),AttrProxy.Create("id",arg10)]),List.ofArray([Client["doc'header"](post),Client["doc'body"](post)]));
    },
    "doc'body":function(post)
    {
     var ats,arg20,arg201,_arg10_,_arg20_;
     ats=List.ofArray([AttrModule.Class("panel-body")]);
     arg20=List.ofArray([Doc.TextView(post.Content.get_View())]);
     arg201=List.ofArray([Doc.TextView(post.EditDate.get_View())]);
     _arg10_=List.ofArray([AttrModule.Class("btn btn-default")]);
     _arg20_=function()
     {
      Var1.Set(Client.rvUsername(),Var1.Get(post.Content));
      jQuery("#blogItems").hide();
      jQuery("#loginform").removeClass("divhidden").addClass("divshown");
      return console?console.log(1):undefined;
     };
     return Doc.Element("div",ats,List.ofArray([Doc.Element("div",[],arg20),Doc.Element("p",[],arg201),Doc.Button("New Item",_arg10_,_arg20_)]));
    },
    "doc'header":function(post)
    {
     var arg20;
     arg20=List.ofArray([Doc.TextView(post.Title.get_View())]);
     return Doc.Element("div",List.ofArray([AttrModule.Class("panel-heading")]),List.ofArray([Doc.Element("div",[],arg20)]));
    },
    "doc'main'menu":Runtime.Field(function()
    {
     var ats,ats1,ats2,arg20,arg201,arg202,ats3,ats4,arg203,arg204,arg205,arg206,arg207,arg208,arg209;
     ats=List.ofArray([AttrModule.Class("nav navbar-nav")]);
     ats1=List.ofArray([AttrModule.Class("dropdown open")]);
     ats2=List.ofArray([AttrModule.Class("dropdown-menu")]);
     arg20=List.ofArray([Doc.TextNode("System Configuration")]);
     arg201=List.ofArray([Doc.TextNode("System Configuration")]);
     arg202=List.ofArray([Doc.TextNode("System Configuration")]);
     ats3=List.ofArray([AttrModule.Class("dropdown open")]);
     ats4=List.ofArray([AttrModule.Class("dropdown-menu")]);
     arg203=List.ofArray([Doc.TextNode("Interface report")]);
     arg204=List.ofArray([Doc.TextNode("Process Schedule Maintenance")]);
     arg205=List.ofArray([Doc.TextNode("Calendar Management")]);
     arg206=List.ofArray([Doc.TextNode("System Utility")]);
     arg207=List.ofArray([Doc.TextNode("System Maintenance")]);
     arg208=List.ofArray([Doc.TextNode("Payroll Process Management")]);
     arg209=List.ofArray([Doc.TextNode("Human Resource Management")]);
     return Doc.Element("ul",ats,List.ofArray([Doc.Element("li",ats1,List.ofArray([Doc.Element("a",List.ofArray([AttrModule.Class("dropdown-toggle")]),List.ofArray([Doc.TextNode("System Configuration")])),Doc.Element("ul",ats2,List.ofArray([Doc.Element("li",[],arg20),Doc.Element("li",[],arg201),Doc.Element("li",[],arg202)]))])),Doc.Element("li",ats3,List.ofArray([Doc.Element("a",List.ofArray([AttrModule.Class("dropdown-toggle")]),List.ofArray([Doc.TextNode("System Utility")])),Doc.Element("ul",ats4,List.ofArray([Doc.Element("li",[],arg203),Doc.Element("li",[],arg204),Doc.Element("li",[],arg205)]))])),Doc.Element("li",[],arg206),Doc.Element("li",[],arg207),Doc.Element("li",[],arg208),Doc.Element("li",[],arg209)]));
    }),
    "doc'nav'left":Runtime.Field(function()
    {
     var arg20;
     arg20=List.ofArray([Doc.TextNode("jkhkjhjk")]);
     return Doc.Element("div",[],arg20);
    }),
    iid:Runtime.Field(function()
    {
     return 5;
    }),
    loaddata:Runtime.Field(function()
    {
     var x,arg00;
     x=Client["v'menu"]().get_View();
     arg00=function()
     {
      return Concurrency.Delay(function()
      {
       var x1;
       x1=AjaxRemotingProvider.Async("Application2:1",[0,"WEBSITE"]);
       return Concurrency.Bind(x1,function(_arg1)
       {
        var patternInput,english,a;
        patternInput=_arg1.$0;
        patternInput[4];
        patternInput[0];
        patternInput[3];
        english=patternInput[1];
        patternInput[2];
        _arg1.$0;
        a="trying to get menu from server "+english;
        console?console.log(a):undefined;
        return Concurrency.Return(Doc.get_Empty());
       });
      });
     };
     return View.MapAsync(arg00,x);
    }),
    post1:Runtime.Field(function()
    {
     return{
      Id:1,
      Title:Var.Create("111ssdfds"),
      Content:Var.Create("333fsfsdf"),
      CreateDate:"2015-01-01",
      EditDate:Var.Create("2015-01-01"),
      Num:Var.Create(1),
      EditedTitle:Var.Create("dsadsdsa"),
      EditedContent:Var.Create("dsadsad"),
      IsEditMode:Var.Create(false)
     };
    }),
    post2:Runtime.Field(function()
    {
     return{
      Id:2,
      Title:Var.Create("222ssdfds"),
      Content:Var.Create("44444fsfsdf"),
      CreateDate:"2015-01-01",
      EditDate:Var.Create("2015-01-01"),
      Num:Var.Create(1),
      EditedTitle:Var.Create("dsadsdsa"),
      EditedContent:Var.Create("dsadsad"),
      IsEditMode:Var.Create(false)
     };
    }),
    postList:Runtime.Field(function()
    {
     var arg00,arg10;
     arg00=function(i)
     {
      return i.Id<<0;
     };
     arg10=Runtime.New(T,{
      $:0
     });
     return ListModel.Create(arg00,arg10);
    }),
    rvSubmit:Runtime.Field(function()
    {
     return Var.Create(null);
    }),
    rvUsername:Runtime.Field(function()
    {
     return Var.Create("");
    }),
    "v'blog":Runtime.Field(function()
    {
     var arg00,arg10;
     arg00=function()
     {
      return Concurrency.Delay(function()
      {
       return Concurrency.Return(Client.postList());
      });
     };
     View.get_Do();
     arg10=View1.Const(1);
     return View.MapAsync(arg00,arg10);
    }),
    "v'menu":Runtime.Field(function()
    {
     return Var.Create("");
    })
   }
  }
 });
 Runtime.OnInit(function()
 {
  console=Runtime.Safe(Global.console);
  alert=Runtime.Safe(Global.alert);
  Forms=Runtime.Safe(Global.WebSharper.Forms);
  Pervasives=Runtime.Safe(Forms.Pervasives);
  Form1=Runtime.Safe(Forms.Form1);
  Form=Runtime.Safe(Forms.Form);
  Validation=Runtime.Safe(Forms.Validation);
  List=Runtime.Safe(Global.WebSharper.List);
  Bootstrap=Runtime.Safe(Forms.Bootstrap);
  Controls=Runtime.Safe(Bootstrap.Controls);
  T=Runtime.Safe(List.T);
  Application2=Runtime.Safe(Global.Application2);
  Client=Runtime.Safe(Application2.Client);
  UI=Runtime.Safe(Global.WebSharper.UI);
  Next=Runtime.Safe(UI.Next);
  AttrProxy=Runtime.Safe(Next.AttrProxy);
  Simple=Runtime.Safe(Controls.Simple);
  Doc=Runtime.Safe(Next.Doc);
  Var=Runtime.Safe(Next.Var);
  Submitter=Runtime.Safe(Next.Submitter);
  Remoting=Runtime.Safe(Global.WebSharper.Remoting);
  AjaxRemotingProvider=Runtime.Safe(Remoting.AjaxRemotingProvider);
  Concurrency=Runtime.Safe(Global.WebSharper.Concurrency);
  View=Runtime.Safe(Next.View);
  AttrModule=Runtime.Safe(Next.AttrModule);
  Seq=Runtime.Safe(Global.WebSharper.Seq);
  ListModel1=Runtime.Safe(Next.ListModel1);
  String=Runtime.Safe(Global.String);
  Var1=Runtime.Safe(Next.Var1);
  jQuery=Runtime.Safe(Global.jQuery);
  ListModel=Runtime.Safe(Next.ListModel);
  return View1=Runtime.Safe(Next.View1);
 });
 Runtime.OnLoad(function()
 {
  Client["v'menu"]();
  Client["v'blog"]();
  Client.rvUsername();
  Client.rvSubmit();
  Client.postList();
  Client.post2();
  Client.post1();
  Client.loaddata();
  Client.iid();
  Client["doc'nav'left"]();
  Client["doc'main'menu"]();
  Client.cls();
  Client.addNewBlog();
  return;
 });
}());
