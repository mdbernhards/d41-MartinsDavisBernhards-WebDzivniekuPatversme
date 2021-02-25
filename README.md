# Dzivnieku patversmes mājaslapa

## Projekta apraksts
Šis ir projekts veidots PIKC "Rīgas Valsts tehnikums" kvalifikācijas darbam. Mājaslapa ar datubāzi domāta dzivnieku patversmēm dzivnieku adopcijai. Var saglabāt informāciju par dzivniekiem, viņu raksturu un slimības, informāciju var mainīt.
Var apskatīt šos dzivniekus, kontaktēt patversmi par tiem. Var apskatīt informāciju par pašu patversmi, tās atrašanās vietu, numuru.

Funkcijas:
- Dzivnieku informācijas saglabāšana datubāzē
- Patversmes informācijas saglabāšana datubāzē
- Saglabātās informācijas rediģēšana
- Dzivnieku meklēšana ar filtriem, kā suga, krāsa, patversme kurā atrodās
- Lietotāji var apskatīt informāciju par dzivniekiem un patversmēm nereģistrējoties
- Reģistrējoties var aizsūtīt epastu noteiktajai patversmei par dzīvnieku
- Mājaslapas kontrole, kā Administratoram un kā Lietotājam
- Var rakstīt rakstus par patversmes jaunākajām ziņām

## Izmantotās valodas
Angļu valoda:
- Datubāzes, tās tabulu un lauku nosaukumi
- Programas failu nosaukumi
- Kods(funkcijas, metodes, objekti, u.c.)
- "Commit messages"

Latviešu valoda:
- Dati datubāzē
- Teksts kodā ("string")
- Viss ko parasts lietotājs var redzēt

## Izmantotās tehnoloģijas
Projektā izmantotās tehnoloģijas:
- C#
- ASP.NET MVC
- MySQL
- HTML
- CSS
- JS

Projektā izmantotie NuGet paplašinājumi:
- [AutoMapper](https://www.nuget.org/packages/AutoMapper/) - ViewModel un Model mapping
- [Pomelo.EntityFrameworkCore.MySql](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/) - Projekta savienošana ar MySql datubāzi
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) - Objektu pārveidošana Json formātā un atpakaļ
- [Microsoft.AspNetCore.Identity.UI](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.UI/) - Autorizācijas un piekļuves kontrolēšanai
- [TinyMCE](https://www.nuget.org/packages/TinyMCE/) - Teksta redaktors

## Izmantotie avoti
- [Datubāzes pievienošana](https://www.c-sharpcorner.com/article/how-to-connect-mysql-with-asp-net-core/)
- [ASP.NET Identity savienošana ar MySQL](https://www.c-sharpcorner.com/article/using-asp-net-core-3-0-identity-with-mysql/)
- ['Scaffolding' izveidošana priekš Identity](https://stackoverflow.com/questions/50802781/where-are-the-login-and-register-pages-in-an-aspnet-core-scaffolded-app)
- [Profila apstiprināšana ar nosūtītu E-pastu](https://docs.microsoft.com/lv-lv/aspnet/core/security/authentication/accconfirm?view=aspnetcore-5.0&tabs=visual-studio)
- [Identity Paziņojumu priekš lietotāja tulkošana](https://stackoverflow.com/questions/19961648/how-to-localize-asp-net-identity-username-and-password-error-messages)
- [Automapper uzstādīšana](https://stackoverflow.com/questions/40275195/how-to-set-up-automapper-in-asp-net-core)
- [DropDown izveidošana razor view + informācija par ViewModels](https://stackoverflow.com/questions/12519280/using-a-foreign-key-in-dropdown-in-mvc)
- [Slēptu vērtību padošana no view uz controller](https://stackoverflow.com/questions/39405527/how-can-i-pass-hidden-field-value-from-view-to-controller-asp-net-mvc-5)
- [Lietotāju un lomu izveidošana](https://stackoverflow.com/questions/34343599/how-to-seed-users-and-roles-with-code-first-migration-using-identity-asp-net-cor)
- [Uz lomām bāzēta autorizācija](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-5.0#policy-based-role-checks)
- [Listes sakārtošana, filstrēšana un paging](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-5.0)
- [Validācija](https://www.tutorialsteacher.com/mvc/implement-validation-in-asp.net-mvc)
- [Datuma validācija](https://stackoverflow.com/questions/46184818/dataanotation-to-validate-a-model-how-do-i-validate-it-so-that-the-date-is-not)
- [Attēlu Augšuplāde](https://stackoverflow.com/questions/47185920/upload-image-in-asp-net-core)
- [Attēlu Validācija](https://stackoverflow.com/questions/56588900/how-to-validate-uploaded-file-in-asp-net-core)
- [Noklusējuma attēls](https://stackoverflow.com/questions/717734/best-way-to-display-default-image-if-specified-image-file-is-not-found)
- [TinyMCE teksta rediģētāja pievienošana](https://forums.asp.net/t/2100291.aspx?Using+HTML+editor+in+MVC+NET+core)
- [Identity modeļu papildināšana un pielāgošana](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0)
- [Relatīvi attēlu ceļi](https://stackoverflow.com/questions/317315/asp-net-mvc-relative-paths)
- [ASP .NET dokumentācija](https://docs.microsoft.com/en-us/aspnet/)

## Uzstādīšanas instrukcijas
- Vel nav