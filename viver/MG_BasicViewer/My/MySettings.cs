// Decompiled with JetBrains decompiler
// Type: MacGen.My.MySettings
// Assembly: MG_BasicViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 995351A4-6479-4341-A2E3-44F14AEC55FB
// Assembly location: C:\Users\tridb\Desktop\BTP\bin\Debug\MG_BasicViewer.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MacGen.My
{
  [CompilerGenerated]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
  internal sealed class MySettings : ApplicationSettingsBase
  {
    private static MySettings defaultInstance = (MySettings) SettingsBase.Synchronized((SettingsBase) new MySettings());

    [DebuggerNonUserCode]
    public MySettings()
    {
    }

    public static MySettings Default => MySettings.defaultInstance;
  }
}
