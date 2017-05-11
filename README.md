Espresso / Espresso-Cup
===============

***V8 js engine with C# (in-process), => Espresso.***

***NodeJS engine with C# (in-process), => Espresso-Cup.***


**Espresso** (former VroomJS) is a bridge between the .NET CLR (think C# or F#) and the V8 Javascript
engine that uses P/Invoke and a thin C layer to avoid the need to recompile V8
C++ using the MS Managed C++ compiler. That means that Espresso is Mono-friendly
because doesn't use any feature that will make it run only on MS.NET.

With Espresso it is possible to execute arbitrary javascript code and get the
result as a managed primitive type (for integers, numbers, strings, dates and
arrays of primitive types) or as a `JsObject` wrapper that allows to
dynamically access properties and call functions on Javascript objects.

Each `JsEngine` is an isolated V8 context and all objects allocated on the
Javascript side are persistent over multiple calls. It is possible to set and
get global variables. Variable values can be primitive types, CLR objects or
`JsObjects` wrapping Javascript objects. CLR instances are kept alive as long
as used in Javascript code (so it isn't required to track them in client code:
they won't be garbage collected as long as references on the V8 side) and it is
possible to access their properties and call methods from JS code.

**Current V8 Engine => We use V8 from node v7.6.0**

Examples
--------

Execute some Javascript:

	using (var js = new JsEngine()) {
		var x = (int)js.Execute("3.14159+2.71828");
		Console.WriteLine(x);  // prints 5.85987
	}

Create and return a Javascript object, then call a method on it:

	using (var js = new JsEngine()) {
		// Create a global variable on the JS side.
		js.Execute("var x = {'answer':42, 'tellme':function (x) { return x+' '+this.answer; }}");
		// Get it and use "dynamic" to tell the compiler to use runtime binding.
		dynamic x = js.GetVariable("x");
		// Call the method and print the result. This will print:
		// "What is the answer to ...? 42"
		Console.WriteLine(x.tellme("What is the answer to ...?"));
	}

Access properties and call methods on CLR objects from Javascript:

	class Test
	{
		public int Value { get; set; }
		public void PrintValue(string msg)
		{
			Console.WriteLine(msg+" "+Value);
		}
	}
	
	using (var js = new JsEngine()) {
		js.SetVariable("m", new Test());
		// Sets the property from Javascript.
		js.Execute("m.Value = 42");
		// Call a method on the CLR object from Javascript. This prints:
		// "And the answer is (again!): 42"
		js.Execute("m.PrintValue('And the answer is (again!):')");
	}


---------------

**Espresso-Cup** is special edition of the Espresso, 
It is NodeJS in dll form + Espresso Code,
so you can run NodeJS app in-process with .NET Code

see example, run nodejs http server 
![slide18](https://cloud.githubusercontent.com/assets/9301044/21955739/95196ecc-daa3-11e6-8e91-c0f443ac0165.png)
![slide19](https://cloud.githubusercontent.com/assets/9301044/21955741/951b7442-daa3-11e6-932f-a76dbeb9358d.PNG)



see how to build it at https://github.com/CompilerKit/Espresso/wiki/Build-NodeJS-for-Espresso-Cup




 
