using System.Reflection;
using System.Security;
using System.Security.Permissions;

[assembly: SecurityRules(SecurityRuleSet.Level1)]
[assembly: AssemblyVersion("0.0.0.0")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
