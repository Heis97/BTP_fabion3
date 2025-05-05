// Decompiled with JetBrains decompiler
// Type: MacGen.My.Resources.Resources
// Assembly: MG_BasicViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 995351A4-6479-4341-A2E3-44F14AEC55FB
// Assembly location: C:\Users\tridb\Desktop\BTP\bin\Debug\MG_BasicViewer.dll

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MacGen.My.Resources
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
  [StandardModule]
  [HideModuleName]
  [DebuggerNonUserCode]
  internal sealed class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) MacGen.My.Resources.Resources.resourceMan, (object) null))
          MacGen.My.Resources.Resources.resourceMan = new ResourceManager("MacGen.Resources", typeof (MacGen.My.Resources.Resources).Assembly);
        return MacGen.My.Resources.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => MacGen.My.Resources.Resources.resourceCulture;
      set => MacGen.My.Resources.Resources.resourceCulture = value;
    }
  }
}
