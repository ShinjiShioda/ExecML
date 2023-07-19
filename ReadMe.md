# EXECML Execute Machine Language Console Program  

---
THIS PROGRAM BASED ON 
[Unknown6656](https://gist.github.com/Unknown6656) / [execute-byte-array.cs](https://gist.github.com/Unknown6656/a42a810d4283208c3c21c632fb16c3f9)
I am only write few codes.

---

This is Machine Language codes execute program.You Can Run Machine Language codes on Console.
これは、機械語を実行するコンソールプログラムです。コマンドラインに機械語コードを16進数で記述することで機械語関数を呼び出して実行します。

For example

    0f bc c1                bsf    %ecx,%eax
    0f bd ca                bsr    %edx,%ecx
    88 cc                   mov    %cl,%ah
    c3                      ret

We get "0F BC C1 0F BD CA 88 CC C3"
上記のプログラムから16進コード「0F BC C1 0F BD CA 88 CC C3」が作られます

Now run to ECX=0x80000000, EDX=0x10
RCX、RDXレジスタに値を入れて実行します。

    PS7.3>ExecML.exe 80000000 10 0f bc c1 0f bd ca 88 cc c3
    000000000000041F
    PS7.3>

Machine Language Codes rutine will call by x64 convention.

Reference
[x64 calling convention | Microsoft Learn](https://learn.microsoft.com/en-us/cpp/build/x64-calling-convention?view=msvc-170)
[x64 での呼び出し規則 | Microsoft Learn](https://learn.microsoft.com/ja-jp/cpp/build/x64-calling-convention?view=msvc-170)

### How to Execute

No install required.
インストールは必要ありません。

    ExecML.exe ECX_HEX_Value EDX_HEX_Value HEX HEX ...

ECX_HEX_Value : 
* Argument #1 for Machine Langame Function. Store to ECX register and Call.
* 機械語関数に渡される１つめの引数。これはECXレジスタにセットされます。

EDX_HEX_Value : 
* Argument #2 for Machine Langame Function. Store to EDX register and Call.
* 機械語関数に渡される２つめの引数。これはEDXレジスタにセットされます。

HEX : 
* Machine Language Codes in Hexiaditimal.
* RET(0xC3) instruction add to Code. It return to main program.
*    機械語コードを表す16進数をスペースで区切っていれてください。
*    機械語コードの最後には、自動的にRET命令が付加され、これでメインプログラムに戻ります。

Machine Language code executed in function.ExecML return RAX register value in HEX.
機械語コードは、関数の中で実行されます。ExecMLは、戻ってきたRAXレジスタの内容を16進数で表示します。

### How to Build

To build from source, You must install .NET SDK
Use DOTNET command（.NET CLI） in .NET SDK

[.NET SDK の概要 | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/core/sdk)
[.NET SDK overview | Microsoft Learn]( https://learn.microsoft.com/en-US/dotnet/core/sdk)

プログラムのビルド
Build from source

    dotnet build -c release -r win-x64 --no-self-contained

最終実行形式の作成
Publish Code

    // exe directory is require at same directory with ExecML.csproj.
    dotnet publish -c release -r win-x64 -o .\exe --no-self-contained