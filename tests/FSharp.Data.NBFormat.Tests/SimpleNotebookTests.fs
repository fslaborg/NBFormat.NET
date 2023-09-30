namespace FSharp.Data.NBFormat.Tests.notebooks

open TestUtils
open System.Text.Json
open FSharp.Data.NBFormat
open Xunit

module FSharpNotebook =
    let notebookJson = getEmbeddedResource "data.simple.polyglot_notebook_simple_fsharp.ipynb"
    let notebook = Serialization.deserializeNotebook notebookJson

    module BasicTests = 
        [<Fact>]
        let ``can read embedded resource`` () =
            Assert.NotNull notebookJson

        [<Fact>]
        let ``has 2 cells`` () =
            Assert.Equal(2, notebook.Cells.Length)

    module CellTests =

        [<Fact>]
        let ``first cell is a markdown cell`` () =
            Cell.isMarkdownCell notebook.Cells.[0]

        [<Fact>]
        let ``second cell is a code cell`` () =
            Cell.isCodeCell notebook.Cells.[1]

        [<Fact>]
        let ``first cell has correct source`` () =
            Cell.hasSource [
                "# Markdown"
            ] notebook.Cells.[0]

        [<Fact>]
        let ``second cell has correct source`` () =
            Cell.hasSource [
                "let a = 42\n"
                "\n"
                "a"
            ] notebook.Cells.[1]

        module OutputTests =
                
            [<Fact>]
            let ``first cell has no output`` () =
                Assert.True(notebook.Cells.[0].Outputs.IsNone)
                    
            [<Fact>]
            let ``second cell has 1 output`` () =
                Assert.Equal(1, notebook.Cells.[1].Outputs.Value.Length)
    
            [<Fact>]
            let ``second cell output is display_data`` () =
                Output.isDisplayData notebook.Cells.[1].Outputs.Value.[0]
    
            [<Fact>]
            let ``second cell output has data`` () =
                Output.hasData notebook.Cells.[1].Outputs.Value.[0]

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
                    notebook.Cells.[1].Outputs.Value.[0]

module CSharpNotebook =
    let notebookJson = getEmbeddedResource "data.simple.polyglot_notebook_simple_csharp.ipynb"
    let notebook = Serialization.deserializeNotebook notebookJson

    module BasicTests = 
        [<Fact>]
        let ``can read embedded resource`` () =
            Assert.NotNull notebookJson

        [<Fact>]
        let ``has 2 cells`` () =
            Assert.Equal(2, notebook.Cells.Length)

    module CellTests =

        [<Fact>]
        let ``first cell is a markdown cell`` () =
            Cell.isMarkdownCell notebook.Cells.[0]

        [<Fact>]
        let ``second cell is a code cell`` () =
            Cell.isCodeCell notebook.Cells.[1]

        [<Fact>]
        let ``first cell has correct source`` () =
            Cell.hasSource [
                "# Markdown"
            ] notebook.Cells.[0]

        [<Fact>]
        let ``second cell has correct source`` () =
            Cell.hasSource [
                "var a = 42;\n"
                "\n"
                "a"
            ] notebook.Cells.[1]

        module OutputTests =
                
            [<Fact>]
            let ``first cell has no output`` () =
                Assert.True(notebook.Cells.[0].Outputs.IsNone)
                    
            [<Fact>]
            let ``second cell has 1 output`` () =
                Assert.Equal(1, notebook.Cells.[1].Outputs.Value.Length)
    
            [<Fact>]
            let ``second cell output is display_data`` () =
                Output.isDisplayData notebook.Cells.[1].Outputs.Value.[0]
    
            [<Fact>]
            let ``second cell output has data`` () =
                Output.hasData notebook.Cells.[1].Outputs.Value.[0]

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
                    notebook.Cells.[1].Outputs.Value.[0]

module PolyglotNotebook =
    let notebookJson = getEmbeddedResource "data.simple.polyglot_notebook_simple.ipynb"
    let notebook = Serialization.deserializeNotebook notebookJson

    module BasicTests = 
        [<Fact>]
        let ``can read embedded resource`` () =
            Assert.NotNull notebookJson

        [<Fact>]
        let ``has 4 cells`` () =
            Assert.Equal(4, notebook.Cells.Length)

    module CellTests =

        [<Fact>]
        let ``first cell is a markdown cell`` () =
            Cell.isMarkdownCell notebook.Cells.[0]

        [<Fact>]
        let ``second cell is a code cell`` () =
            Cell.isCodeCell notebook.Cells.[1]

        [<Fact>]
        let ``third cell is a markdown cell`` () =
            Cell.isMarkdownCell notebook.Cells.[2]

        [<Fact>]
        let ``fourth cell is a code cell`` () =
            Cell.isCodeCell notebook.Cells.[3]

        [<Fact>]
        let ``first cell has correct source`` () =
            Cell.hasSource [
                "# FSharp"
            ] notebook.Cells.[0]

        [<Fact>]
        let ``second cell has correct source`` () =
            Cell.hasSource [
                "let a = 42\n"
                "\n"
                "a"
            ] notebook.Cells.[1]

        [<Fact>]
        let ``third cell has correct source`` () =
            Cell.hasSource [
                "# CSharp"
            ] notebook.Cells.[2]

        [<Fact>]
        let ``fourth cell has correct source`` () =
            Cell.hasSource [
                "var a = 42;\n"
                "\n"
                "a"
            ] notebook.Cells.[3]

        module OutputTests =
                
            [<Fact>]
            let ``first cell has no output`` () =
                Assert.True(notebook.Cells.[0].Outputs.IsNone)
                    
            [<Fact>]
            let ``second cell has 1 output`` () =
                Assert.Equal(1, notebook.Cells.[1].Outputs.Value.Length)
            
            [<Fact>]
            let ``third cell has no output`` () =
                Assert.True(notebook.Cells.[2].Outputs.IsNone)
                    
            [<Fact>]
            let ``fourth cell has 1 output`` () =
                Assert.Equal(1, notebook.Cells.[3].Outputs.Value.Length)
    
            [<Fact>]
            let ``second cell output is display_data`` () =
                Output.isDisplayData notebook.Cells.[1].Outputs.Value.[0]
            
            [<Fact>]
            let ``fourth cell output is display_data`` () =
                Output.isDisplayData notebook.Cells.[3].Outputs.Value.[0]
    
            [<Fact>]
            let ``second cell output has data`` () =
                Output.hasData notebook.Cells.[1].Outputs.Value.[0]
    
            [<Fact>]
            let ``fourth cell output has data`` () =
                Output.hasData notebook.Cells.[3].Outputs.Value.[0]

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
                    notebook.Cells.[1].Outputs.Value.[0]
                    
            [<Fact>]
            let ``fourth cell output has correct data`` () =
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
                    notebook.Cells.[3].Outputs.Value.[0]