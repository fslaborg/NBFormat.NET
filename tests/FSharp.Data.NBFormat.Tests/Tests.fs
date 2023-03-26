namespace FSharp.Data.NBFormat

module Tests =
    open System
    open Xunit

    open FSharp.Data.NBFormat
    open System.Reflection
    open System.IO
    open TestUtils

    open System.Text.Json
    open System.Text.Json.Serialization

    let assembly = Assembly.GetExecutingAssembly()

    let getEmbeddedResource file = 
        use str = assembly.GetManifestResourceStream($"FSharp.Data.NBFormat.Tests.{file}")
        use r = new StreamReader(str)
        r.ReadToEnd()


    module SimpleNotebookTests =
        let notebookJson = getEmbeddedResource "data.polyglot_notebook_simple_fsharp.ipynb"
        let simpleNotebook = Serialiation.deserializeNotebook notebookJson

        module BasicTests = 
            [<Fact>]
            let ``can read embedded resource`` () =
                Assert.NotNull notebookJson

            [<Fact>]
            let ``has 2 cells`` () =
                Assert.Equal(2, simpleNotebook.Cells.Length)

        module CellTests =

            [<Fact>]
            let ``first cell is a markdown cell`` () =
                Cell.isMarkdownCell simpleNotebook.Cells.[0]

            [<Fact>]
            let ``second cell is a code cell`` () =
                Cell.isCodeCell simpleNotebook.Cells.[1]

            [<Fact>]
            let ``first cell has correct source`` () =
                Cell.hasSource [
                    "# Markdown"
                ] simpleNotebook.Cells.[0]

            [<Fact>]
            let ``second cell has correct source`` () =
                Cell.hasSource [
                    "let a = 42\n"
                    "\n"
                    "a"
                ] simpleNotebook.Cells.[1]

            module OutputTests =
                
                [<Fact>]
                let ``first cell has no output`` () =
                    Assert.True(simpleNotebook.Cells.[0].Outputs.IsNone)
                    
                [<Fact>]
                let ``second cell has 1 output`` () =
                    Assert.Equal(1, simpleNotebook.Cells.[1].Outputs.Value.Length)
    
                [<Fact>]
                let ``second cell output is display_data`` () =
                    Output.isDisplayData simpleNotebook.Cells.[1].Outputs.Value.[0]
    
                [<Fact>]
                let ``second cell output has data`` () =
                    Output.hasData simpleNotebook.Cells.[1].Outputs.Value.[0]

                [<Fact>]
                let ``second cell output has correct data`` () =
                    Output.forData (
                        MimeBundle.hasValue<string list> 
                            "text/html"
                            [
                                "<div class=\"dni-plaintext\"><pre>42</pre></div><style>\r\n"
                                ".dni-code-hint {\r\n"
                                "    font-style: italic;\r\n"
                                "    overflow: hidden;\r\n"
                                "    white-space: nowrap;\r\n"
                                "}\r\n"
                                ".dni-treeview {\r\n"
                                "    white-space: nowrap;\r\n"
                                "}\r\n"
                                ".dni-treeview td {\r\n"
                                "    vertical-align: top;\r\n"
                                "    text-align: start;\r\n"
                                "}\r\n"
                                "details.dni-treeview {\r\n"
                                "    padding-left: 1em;\r\n"
                                "}\r\n"
                                "table td {\r\n"
                                "    text-align: start;\r\n"
                                "}\r\n"
                                "table tr { \r\n"
                                "    vertical-align: top; \r\n"
                                "    margin: 0em 0px;\r\n"
                                "}\r\n"
                                "table tr td pre \r\n"
                                "{ \r\n"
                                "    vertical-align: top !important; \r\n"
                                "    margin: 0em 0px !important;\r\n"
                                "} \r\n"
                                "table th {\r\n"
                                "    text-align: start;\r\n"
                                "}\r\n"
                                "</style>"
                            ]
                            
                    )
                        simpleNotebook.Cells.[1].Outputs.Value.[0]