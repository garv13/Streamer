﻿#pragma checksum "C:\Users\garvj\documents\visual studio 2015\Projects\Heist\Heist\Purchased.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FFF68DDEC9B509743D5A69F4624A3E08"
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
    partial class Purchased : 
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
                    #line 42 "..\..\..\Purchased.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.HamburgerButton).Click += this.HamburgerButton_Click;
                    #line default
                }
                break;
            case 3:
                {
                    this.MenuButton2 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 45 "..\..\..\Purchased.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.MenuButton2).Click += this.MenuButton2_Click;
                    #line default
                }
                break;
            case 4:
                {
                    this.MenuButton3 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 48 "..\..\..\Purchased.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.MenuButton3).Click += this.MenuButton3_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.MenuButton4 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 51 "..\..\..\Purchased.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.MenuButton4).Click += this.MenuButton4_Click;
                    #line default
                }
                break;
            case 6:
                {
                    global::Windows.UI.Xaml.Controls.RadioButton element6 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 54 "..\..\..\Purchased.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)element6).Click += this.MenuButton1_Click;
                    #line default
                }
                break;
            case 7:
                {
                    this.MenuButton5 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 57 "..\..\..\Purchased.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.MenuButton5).Click += this.MenuButton5_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.MenuButton6 = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 60 "..\..\..\Purchased.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.MenuButton6).Click += this.MenuButton6_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.LoadingBar = (global::Windows.UI.Xaml.Controls.ProgressBar)(target);
                }
                break;
            case 10:
                {
                    this.StoreListView = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    #line 93 "..\..\..\Purchased.xaml"
                    ((global::Windows.UI.Xaml.Controls.GridView)this.StoreListView).ItemClick += this.StoreListView_ItemClick;
                    #line default
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

