﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RDSSNLSMPUtilsClasses.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("c:\\RDSS\\Settings\\AvidAc\\RDSSPaymentConfig.xml")]
        public string SettingsFile {
            get {
                return ((string)(this["SettingsFile"]));
            }
            set {
                this["SettingsFile"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://trans.merchantpartners.com/Web/services/TransactionService")]
        public string RDDSNLSMPUtilsClasses_com_merchantpartners_trans1_TransactionSOAPBindingImplService {
            get {
                return ((string)(this["RDDSNLSMPUtilsClasses_com_merchantpartners_trans1_TransactionSOAPBindingImplServi" +
                    "ce"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://ws.cyberridge.com/AvidAcceptancewebservice/Service.asmx")]
        public string RDDSNLSMPUtilsClasses_com_cyberridge_ws_Service {
            get {
                return ((string)(this["RDDSNLSMPUtilsClasses_com_cyberridge_ws_Service"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://wsvar.paymentech.net/PaymentechGateway")]
        public string RDDSNLSMPUtilsClasses_net_paymentech_wsvar_PaymentechGateway {
            get {
                return ((string)(this["RDDSNLSMPUtilsClasses_net_paymentech_wsvar_PaymentechGateway"]));
            }
        }
    }
}
