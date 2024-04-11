var people = new Arr(
    new { name = "Astra", age = 50 },
    new { name = "Cecilia", age = 30 },
    new { name = "Bertil", age = 20 }
);

people[1].lastName = "Carlsson";

var aha = people.Map((x, i) => new
{
    SPREAD = x,
    name = x.name + " Lisa" + i
});

Con.Log(aha.Length(), aha);

var oho = people.Filter(x => x.name != "Bertil");

Con.Log(oho.Length(), oho);


var nums = new Arr(1, 2, 3, 3, 4, 5, 6, 10, 0);
Con.Log("nums", nums.FindIndex(x => x == 0));



/*var demo = new Expression[]{
    ()=>people.Length(),
    ()=>people.Push(new {name="Emilia",age=5 }),
    ()=>people.Unshift(new {name="Anna",age=80}),
    ()=>people.Pop(),
    ()=>people.Shift(),
    ()=>people.ToReversed(),
    ()=>people.Reverse(),
    /*()=>people.ToSorted(),
    ()=>people.Sort(),
    ()=>"run:Map", (string x) => x + " Svensson",
    //()=>"run:Filter", (x) => x.name != "Cecilia",
    /*()=>"run:Find",(string x)=> x != "Bertil",
    ()=>"run:FindLast",(string x)=> x != "Bertil",
    ()=>"run:FindIndex",(string x)=> x != "Bertil",
    ()=>"run:FindLastIndex",(string x)=> x != "Bertil",
    ()=>"run:Some",(string x)=> x == "Bertil",
    ()=>"run:Every",(string x)=> x == "Bertil"
};*/

// Run all expressions and log what we are doing
/*var toRun = "";
foreach (var expr in demo)
{
    var rawCode = expr.ToReadableString();
    var code = rawCode.Split("=> ", 2)[1];
    if (toRun != "") { code = "people." + toRun + "(expr)"; toRun = ""; }
    var result = Eval.Execute(code, new { people, expr });
    var strResult = (string)(result + "");
    toRun = strResult.Contains("run:") ? strResult.Split(":")[1] : "";
    if (toRun != "") { continue; }
    Con.Log("");
    var expStr = "(string " + rawCode.Replace(" =>", ") =>");
    code = code.Replace("expr", expStr);
    Con.Log(code != "people" ? code : "",
        result == people ? "" : "-> " + result);
    if (code.Contains("string x")) { continue; }
    Con.Log("people ->", people);
}*/