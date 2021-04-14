# Dzivnieku patversmes mājaslapa

## Projekta apraksts
Šis projekts veidots PIKC "Rīgas Valsts tehnikums" kvalifikācijas darbam. Viegli izmantojama un loģiska mājaslapa ar datubāzi domāta dzivnieku patversmēm, to darbiniekiem un lietotājiem, kas potencjāli vēlas adoptēt dzivnieku. Mājaslapā var saglabāt informāciju par dzivniekiem, viņu raksturu un slimības, informāciju var mainīt.
Lietotājs var apskatīt šos dzivniekus, kontaktēt patversmi par kādu no tiem nosūtot ziņu. Var iegūt informāciju par patversmēm, to atrašanās vietu, telefona numuru.

Funkcijas:
- Dzivnieku informācijas saglabāšana datubāzē
- Patversmes informācijas saglabāšana datubāzē
- Saglabātās informācijas rediģēšana
- Dzivnieku meklēšana ar filtriem, kā: suga, krāsa, vecums, patversme kurā atrodās
- Lietotāji var apskatīt informāciju par dzivniekiem un patversmēm nereģistrējoties
- Reģistrējoties var aizsūtīt Epastu noteiktajai patversmei par dzīvnieku
- Dažādas mājaslapas kontroles opcijas, kā Administratoram un kā Lietotājam
- Darbinieki var veidot rakstus saistītus ar patversmēm, dzīvniekiem

## Izmantotās valodas
Angļu valoda:
- Datubāzes, tās tabulu un lauku nosaukumi
- Programas failu nosaukumi
- Kods(funkcijas, metodes, objekti, u.c.)
- "Commit" ziņojumi

Latviešu valoda:
- Dati datubāzē
- Teksts kodā ("string")
- Visa parastam lietotājam redzamā informācija

## Izmantotās tehnoloģijas
Projektā izmantotās tehnoloģijas:
- ASP.NET Core
- Razor Pages un MVC
- C#
- MySQL
- JavaScript
- HTML
- CSS

Projektā izmantotie NuGet paplašinājumi:
- [AutoMapper](https://www.nuget.org/packages/AutoMapper) - ViewModel un Model savienošanai (mapping)
- [Pomelo.EntityFrameworkCore.MySql](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql) - Projekta savienošana ar MySql datubāzi
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json) - Objektu pārveidošana Json formātā un atpakaļ
- [Microsoft.AspNetCore.Identity.UI](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.UI) - Profila izveidei, autorizācijas un piekļuves kontrolēšanai
- [TinyMCE](https://www.nuget.org/packages/TinyMCE) - Teksta redaktors
- [Microsoft.AspNetCore.Authentication.Google](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.Google) - Autentifikācija ar Google servisiem
- [Microsoft.AspNetCore.Authentication.Facebook](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.Facebook) - Autentifikācija ar Facebook
- [Microsoft.AspNetCore.Authentication.MicrosoftAccount](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.MicrosoftAccount) - Autentifikācija ar Microsoft servisiem

Citi paplašinājumi:
- [qrcode.js](https://davidshimjs.github.io/qrcodejs/) - QR kodu ģenerātors

## Izmantotie avoti
- [Datubāzes pievienošana](https://www.c-sharpcorner.com/article/how-to-connect-mysql-with-asp-net-core)
- [ASP.NET Identity savienošana ar MySQL Datubāzi](https://www.c-sharpcorner.com/article/using-asp-net-core-3-0-identity-with-mysql)
- ['Scaffolding' izveidošana priekš ASP.NET Core Identity](https://stackoverflow.com/questions/50802781/where-are-the-login-and-register-pages-in-an-aspnet-core-scaffolded-app)
- [Profila apstiprināšana ar nosūtītu E-pastu](https://docs.microsoft.com/lv-lv/aspnet/core/security/authentication/accconfirm?view=aspnetcore-5.0&tabs=visual-studio)
- [Lietotājam redzamo Identity paziņojumu un kļūmju tulkošana](https://stackoverflow.com/questions/19961648/how-to-localize-asp-net-identity-username-and-password-error-messages)
- [Automapper uzstādīšana](https://stackoverflow.com/questions/40275195/how-to-set-up-automapper-in-asp-net-core)
- [DropDownList izveidošana razor View + informācija par ViewModels](https://stackoverflow.com/questions/12519280/using-a-foreign-key-in-dropdown-in-mvc)
- [Slēptu vērtību padošana no View uz Controller](https://stackoverflow.com/questions/39405527/how-can-i-pass-hidden-field-value-from-view-to-controller-asp-net-mvc-5)
- [Lietotāju un lomu izveidošana](https://stackoverflow.com/questions/34343599/how-to-seed-users-and-roles-with-code-first-migration-using-identity-asp-net-cor)
- [Uz lomām bāzētas autorizācija izveide](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-5.0#policy-based-role-checks)
- [Listes sakārtošana, filtrēšana un paging izveide](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-5.0)
- [Validācijas pievienošana](https://www.tutorialsteacher.com/mvc/implement-validation-in-asp.net-mvc)
- [Datuma validācija](https://stackoverflow.com/questions/46184818/dataanotation-to-validate-a-model-how-do-i-validate-it-so-that-the-date-is-not)
- [Attēlu Augšuplāde](https://stackoverflow.com/questions/47185920/upload-image-in-asp-net-core)
- [Attēlu Validācija](https://stackoverflow.com/questions/56588900/how-to-validate-uploaded-file-in-asp-net-core)
- [Noklusējuma attēls](https://stackoverflow.com/questions/717734/best-way-to-display-default-image-if-specified-image-file-is-not-found)
- [TinyMCE teksta redaktora pievienošana](https://forums.asp.net/t/2100291.aspx?Using+HTML+editor+in+MVC+NET+core)
- [Identity modeļu papildināšana un pielāgošana](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0)
- [Relatīvi attēlu ceļi](https://stackoverflow.com/questions/317315/asp-net-mvc-relative-paths)
- [QR koda ģenerācija](https://docs.microsoft.com/lv-lv/aspnet/core/security/authentication/identity-enable-qrcodes?view=aspnetcore-5.0)
- [Autentifikācija ar ārējiem servisiem](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/?view=aspnetcore-5.0)
- [Autentifikācija ar Google servisiem](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-5.0)
- [Autentifikācija ar Facebook](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/facebook-logins?view=aspnetcore-5.0)
- [Autentifikācija ar Microsoft servisiem](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/microsoft-logins?view=aspnetcore-5.0)

## Izmantoto tehnoloģiju dokumentācija
- [ASP .NET dokumentācija](https://docs.microsoft.com/en-us/aspnet)

## Uzstādīšanas instrukcijas
- Vel nav