﻿#pragma checksum "C:\Users\garvj\documents\visual studio 2015\Projects\Heist\Heist\About.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "09933038A7782B31983400C8D7B20112"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Heist
{
    partial class About : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.MySplitView = (global::Windows.UI.Xaml.Controls.SplitView)(target);
                }
                break;
            case 2:
                {
                    this.HamburgerButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 22 "..\..\..\About.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.HamburgerButton).Click += this.HamburgerButton_Click;
                    #line default
                }
                break;
            case 3:
                {
                    this.MenuButton2 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 25 "..\..\..\About.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.MenuButton2).Click += this.MenuButton2_Click;
                    #line default
                }
                break;
            case 4:
                {
                    this.MenuButton3 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 28 "..\..\..\About.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.MenuButton3).Click += this.MenuButton3_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.MenuButton4 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 31 "..\..\..\About.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.MenuButton4).Click += this.MenuButton4_Click;
                    #line default
                }
                break;
            case 6:
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element6 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 34 "..\..\..\About.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element6).Click += this.MenuButton1_Click;
                    #line default
                }
                break;
            case 7:
                {
                    this.MenuButton5 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 37 "..\..\..\About.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.MenuButton5).Click += this.MenuButton5_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.MenuButton6 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 40 "..\..\..\About.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.MenuButton6).Click += this.MenuButton6_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.IntroBox = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

