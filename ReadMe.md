# Execute Machine Language Console Program “EXECML”<BR>機械語実行コンソールプログラム「ExecML」

---
THIS PROGRAM BASED ON<BR>[Unknown6656](https://gist.github.com/Unknown6656) / [execute-byte-array.cs](https://gist.github.com/Unknown6656/a42a810d4283208c3c21c632fb16c3f9)<BR>I am only write few codes.

---

This is Machine Language codes execute program.You Can Run Machine Language codes on Console.<BR>これは、機械語を実行するコンソールプログラムです。コマンドラインに機械語コードを16進数で記述することで機械語関数を呼び出して実行します。

For example（例）

    0f bc c1                bsf    %ecx,%eax
    0f bd ca                bsr    %edx,%ecx
    88 cc                   mov    %cl,%ah
    c3                      ret

We get hexadecimal codes "0F BC C1 0F BD CA 88 CC C3".<BR>上記のプログラムから16進コード「0F BC C1 0F BD CA 88 CC C3」が作られます

Now run to ECX=0x80000000, EDX=0x10.<BR>RCX、RDXレジスタに値を入れて実行します。

    PS7.3>ExecML.exe 80000000 10 0f bc c1 0f bd ca 88 cc c3
    000000000000041F
    PS7.3>

Machine Language Codes are called with x64 calling convention.<BR>機械語のルーチンは、x64呼び出し規則により呼び出されます。

Reference/参考情報

[x64 calling convention | Microsoft Learn](https://learn.microsoft.com/en-us/cpp/build/x64-calling-convention?view=msvc-170)

[x64 での呼び出し規則 | Microsoft Learn](https://learn.microsoft.com/ja-jp/cpp/build/x64-calling-convention?view=msvc-170)

## How to Execute / 実行方法
No install required.<BR>インストールは必要ありません。

    ExecML.exe ECX_HEX_Value EDX_HEX_Value HEX HEX ...

ECX_HEX_Value : 
* Argument #1 for Machine Langame Function. Store to ECX register and Call.
* 機械語関数に渡される１つめの引数。これはECXレジスタにセットされます。

EDX_HEX_Value : 
* Argument #2 for Machine Langame Function. Store to EDX register and Call.
* 機械語関数に渡される２つめの引数。これはEDXレジスタにセットされます。

HEX : 
* Machine Language Codes in hexadecimal.
* RET(0xC3) instruction add to Code. It return to main program.
*    機械語コードを表す16進数をスペースで区切っていれてください。
*    機械語コードの最後には、自動的にRET命令が付加され、これでメインプログラムに戻ります。

Machine Language code executed in function.ExecML return RAX register value in HEX.<BR>機械語コードは、関数の中で実行されます。ExecMLは、戻ってきたRAXレジスタの内容を16進数で表示します。

## How to Build / ビルド方法

To build from source, You must install .NET SDK.<BR>ソースコードをビルドするには.NET SDKが必要になります。

Use DOTNET command（.NET CLI） in .NET SDK.<BR>.NET SDKのDOTNETコマンド（.NET CLI）を使います。

[.NET SDK の概要 | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/core/sdk)

[.NET SDK overview | Microsoft Learn]( https://learn.microsoft.com/en-US/dotnet/core/sdk)

Build from source<BR>プログラムのビルド


    dotnet build -c release -r win-x64 --no-self-contained

Publish Code<BR>最終実行形式の作成


    // exe directory is require at same directory with ExecML.csproj.
    dotnet publish -c release -r win-x64 -o .\exe --no-self-contained

## Exe Binary
Executable Binary Publish in my [Blog](https://shinjishioda.blogspot.com/).
実行ファイルは、[ブログ](https://shinjishioda.blogspot.com/)で公開しています。
