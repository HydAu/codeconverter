# Language Conversion Tests
### This file was generated by tests that were run on 3/19/2017 10:05:06 PM.
# Convert CSharp to PowerShell
## Assign a constant to a variable
### Source: CSharp
```csharp
public void Method()
{
    var variable = 1;
	var variable = "1";
}
```
### Target: PowerShell
```powershell
function Method
{
	$variable = 1
	$variable = "1"
}
```

## Declare a method
### Source: CSharp
```csharp
namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method()
        {
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
}
```

## For loop
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method()
        {
            for(int i = 0; i < 100; i++)
            {
                var t = i;
            }
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	for([int]$i = 0; $i -lt 100; $i++)
	{
		$t = $i
	}
}
```

## Throw statement
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Throw
    {
        public void Method()
        {
            throw new Exception("Hey");
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	throw (New-Object -TypeName Exception -ArgumentList "Hey")
}
```

## Using statement
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method()
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 128;
                aes.Key = key;
                aes.IV = IV;
            }
        }

        public void Method2()
        {
            var obj = new object();
            using (obj)
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 128;
                aes.Key = key;
                aes.IV = IV;
            }
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	[AesManaged]$aes = $null
	try
	{
		$aes = (New-Object -TypeName AesManaged)
		$aes.Padding = $PaddingMode.PKCS7
		$aes.KeySize = 128
		$aes.Key = $key
		$aes.IV = $IV
	}
	finally
	{
		$aes.Dispose()
	}
}
function Method2
{
	$obj = (New-Object -TypeName object)
	try
	{
		$aes.Padding = $PaddingMode.PKCS7
		$aes.KeySize = 128
		$aes.Key = $key
		$aes.IV = $IV
	}
	finally
	{
		$obj.Dispose()
	}
}
```

## Try, catch, finally
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method()
        {
            try
            {
                var item = new object();
            }
            catch (Exception ex)
            {
                var item = new object();
            }
            catch
            {
                var item = new object();
            }
            finally
            {
                var item = new object();
            }
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	try
	{
		$item = (New-Object -TypeName object)
	}
	catch [Exception]
	{
		$item = (New-Object -TypeName object)
	}
	catch
	{
		$item = (New-Object -TypeName object)
	}
	finally
	{
		$item = (New-Object -TypeName object)
	}
}
```

## Switch statement
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        void Method()
        {
            int i = 0;
            int x = 1;
            switch (i)
            {
                case 2:
                    x = 2;
                    break;
                case 3:
                    x = 3;
                    break;
                default:
                    break;
            }
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	[int]$i = 0
	[int]$x = 1
	switch ($i)
	{
		2 {
			$x = 2
			
		}
		3 {
			$x = 3
			
		}
		default {
			
		}
	}
}
```

## Access the property of a variable
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class PropertyAccess
    {
        public void Method()
        {
            var timeZoneInfo = new TimeZoneInfo();
            var variable = timeZoneInfo.DisplayName;
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	$timeZoneInfo = (New-Object -TypeName TimeZoneInfo)
	$variable = $timeZoneInfo.DisplayName
}
```

## Declare a method with arguments
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method(string argument, int integer)
        {
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	param([string]$argument, [int]$integer)
}
```

## Create an object
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method()
        {
            new System.Object();
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	(New-Object -TypeName System.Object)
}
```

## Indexer property
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method(string str)
        {
            var item = str[3];
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	param([string]$str)
	$item = $str[3]
}
```

## Converts an incomplete code block successfully.
### Source: CSharp
```csharp
listView1.Items.Add(new ListViewItem(new string[]{"1", "content"}));
listView1.Items.Add(new ListViewItem(new string[]{"4", "content2"}));
listView1.Items.Add(new ListViewItem(new string[]{"2", "content3"}));
```
### Target: PowerShell
```powershell
function Method
{
	$listView1.Items.Add((New-Object -TypeName ListViewItem -ArgumentList @("1","content")))
	$listView1.Items.Add((New-Object -TypeName ListViewItem -ArgumentList @("4","content2")))
	$listView1.Items.Add((New-Object -TypeName ListViewItem -ArgumentList @("2","content3")))
}
```

## While loop with break
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method()
        {
            while (true)
            {
                break;
            }
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	while ($true)
	{
		break
	}
}
```

## Platform invoke signature
### Source: CSharp
```csharp
[DllImport("advapi32.dll", SetLastError = true)]
public static extern bool AbortSystemShutdown(string lpMachineName);

[DllImport("credui", CharSet = CharSet.Unicode)]
public static extern CredUIReturnCodes CredUIPromptForCredentialsW(ref CREDUI_INFO creditUR,
           string targetName,
           IntPtr reserved1,
           int iError,
           StringBuilder userName,
           int maxUserName,
           StringBuilder password,
           int maxPassword,
           [MarshalAs(UnmanagedType.Bool)] ref bool pfSave,
           CREDUI_FLAGS flags);
```
### Target: PowerShell
```powershell
function AbortSystemShutdown
{
	param([string]$lpMachineName)
	Add-Type -TypeDefinition '
		using System;
		using System.Runtime.InteropServices;
		public static class PInvoke {
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern bool AbortSystemShutdown(string lpMachineName);
		}
	'
	[PInvoke]::AbortSystemShutdown($lpMachineName)
}

function CredUIPromptForCredentialsW
{
	param([ref][CREDUI_INFO]$creditUR, [string]$targetName, [IntPtr]$reserved1, [int]$iError, [StringBuilder]$userName, [int]$maxUserName, [StringBuilder]$password, [int]$maxPassword, [ref][bool]$pfSave, [CREDUI_FLAGS]$flags)
	Add-Type -TypeDefinition '
		using System;
		using System.Runtime.InteropServices;
		public static class PInvoke {
			[DllImport("credui", CharSet = CharSet.Unicode)]
			public static extern CredUIReturnCodes CredUIPromptForCredentialsW(ref CREDUI_INFO creditUR,
			string targetName,
			IntPtr reserved1,
			int iError,
			StringBuilder userName,
			int maxUserName,
			StringBuilder password,
			int maxPassword,
			[MarshalAs(UnmanagedType.Bool)] ref bool pfSave,
			CREDUI_FLAGS flags);
		}
	'
	[PInvoke]::CredUIPromptForCredentialsW([ref]$creditUR, $targetName, $reserved1, $iError, $userName, $maxUserName, $password, $maxPassword, [ref]$pfSave, $flags)
}
```

## Common operators
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method()
        {
            var eq = 1 == 2;
            var notEq = 1 != 2;
            var or = 1 == 2 || 2 == 1;
            var and = 1 == 2 && 2 == 1;
            var gt = 1 > 2;
            var lt = 1 < 2;
            var ge = 1 >= 2;
            var le = 1 <= 2;
            var plus = 1 + 1;
            var minus = 1 - 1;
            var bor = 1 | 1;
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	$eq = 1 -eq 2
	$notEq = 1 -ne 2
	$or = 1 -eq 2 -or 2 -eq 1
	$and = 1 -eq 2 -and 2 -eq 1
	$gt = 1 -gt 2
	$lt = 1 -lt 2
	$ge = 1 -ge 2
	$le = 1 -le 2
	$plus = 1 + 1
	$minus = 1 - 1
	$bor = 1 -bor 1
}
```

## Cast operator
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method()
        {
            var myInt = 1;
            var myLong = (long)myInt;
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	$myInt = 1
	$myLong = [long]$myInt
}
```

## Return statement
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public int Method()
        {
            return 1;
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	return 1
}
```

## Array creation initializers
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method()
        {
            var arr = new string[] { "my", "strings" };
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	$arr = @("my","strings")
}
```

## If, Else If, Else
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method()
        {
            if (1 == 2)
            {

            }
            else if ("xyz" == (new Object()))
            {

            }
            else
            {

            }
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	if (1 -eq 2)
	{
	}
	elseif ("xyz" -eq ((New-Object -TypeName Object)))
	{
	}
	else
	{
	}
}
```

## Foreach loop
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method(string[] strings)
        {
            foreach(var item in strings)
            {
                var str = item;
                continue;
            }
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	param([string[]]$strings)
	foreach ($item in $strings)
	{
		$str = $item
		continue
	}
}
```

## Create an object with arugments
### Source: CSharp
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConverter.Test.Languages.CSharp
{
    public class Class
    {
        public void Method()
        {
            new System.Object(myVariable);
        }
    }
}

```
### Target: PowerShell
```powershell
function Method
{
	(New-Object -TypeName System.Object -ArgumentList $myVariable)
}
```

## Declare a method outside of a class or namespace
### Source: CSharp
```csharp
void Method()
{

}
```
### Target: PowerShell
```powershell
function Method
{
}
```
