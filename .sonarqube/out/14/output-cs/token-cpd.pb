÷
yC:\Users\matty\Documents\Development\Uni\Year 5\FYP\Uni-FYP-RestaurantBookingSystem\AuthService\App_Start\WebApiConfig.cs
	namespace 	
AuthService
 
{		 
public

 

static

 
class

 
WebApiConfig

 $
{ 
public 
static 
void 
Register #
(# $
HttpConfiguration$ 5
config6 <
)< =
{ 	
config 
. "
MapHttpAttributeRoutes )
() *
)* +
;+ ,
config 
. 
Routes 
. 
MapHttpRoute &
(& '
name 
: 
$str "
," #
routeTemplate 
: 
$str 6
,6 7
defaults 
: 
new 
{ 
id  "
=# $
RouteParameter% 3
.3 4
Optional4 <
}= >
) 
; 
var 
jsonFormatter 
= 
config  &
.& '

Formatters' 1
.1 2
OfType2 8
<8 9"
JsonMediaTypeFormatter9 O
>O P
(P Q
)Q R
.R S
FirstS X
(X Y
)Y Z
;Z [
jsonFormatter 
. 
SerializerSettings ,
., -
ContractResolver- =
=> ?
new@ C2
&CamelCasePropertyNamesContractResolverD j
(j k
)k l
;l m
} 	
} 
} Ć%
vC:\Users\matty\Documents\Development\Uni\Year 5\FYP\Uni-FYP-RestaurantBookingSystem\AuthService\Auth\AuthRepository.cs
	namespace 	
AuthService
 
. 
Auth 
{ 
public 

class 
AuthRepository 
:  !
IDisposable" -
{ 
private 
AuthContext 
_ctx  
;  !
private 
UserManager 
< 
IdentityUser (
>( )
_userManager* 6
;6 7
private 
RoleManager 
< 
IdentityRole (
>( )
_roleManager* 6
;6 7
public 
AuthRepository 
( 
) 
{ 	
_ctx 
= 
new 
AuthContext "
(" #
)# $
;$ %
_userManager 
= 
new 
UserManager *
<* +
IdentityUser+ 7
>7 8
(8 9
new9 <
	UserStore= F
<F G
IdentityUserG S
>S T
(T U
_ctxU Y
)Y Z
)Z [
;[ \
_roleManager 
= 
new 
RoleManager *
<* +
IdentityRole+ 7
>7 8
(8 9
new9 <
	RoleStore= F
<F G
IdentityRoleG S
>S T
(T U
_ctxU Y
)Y Z
)Z [
;[ \
} 	
public 
async 
Task 
< 
IdentityResult (
>( )
RegisterUser* 6
(6 7
	UserModel7 @
	userModelA J
)J K
{ 	
IdentityUser 
user 
= 
new  #
IdentityUser$ 0
{ 
UserName 
= 
	userModel $
.$ %
UserName% -
}   
;   
var"" 
result"" 
="" 
await"" 
_userManager"" +
.""+ ,
CreateAsync"", 7
(""7 8
user""8 <
,""< =
	userModel""> G
.""G H
Password""H P
)""P Q
;""Q R
return$$ 
result$$ 
;$$ 
}%% 	
public'' 
async'' 
Task'' 
<'' 
IdentityUser'' &
>''& '
FindUser''( 0
(''0 1
string''1 7
userName''8 @
,''@ A
string''B H
password''I Q
)''Q R
{(( 	
IdentityUser)) 
user)) 
=)) 
await))  %
_userManager))& 2
.))2 3
	FindAsync))3 <
())< =
userName))= E
,))E F
password))G O
)))O P
;))P Q
return++ 
user++ 
;++ 
},, 	
public.. 
async.. 
Task.. 
<.. 
bool.. 
>.. 

AssignRole..  *
(..* +
string..+ 1
id..2 4
,..4 5
string..6 <
role..= A
)..A B
{// 	
var00 
res00 
=00 
await00 
_userManager00 (
.00( )
AddToRoleAsync00) 7
(007 8
id008 :
,00: ;
role00< @
)00@ A
;00A B
return11 
true11 
;11 
}22 	
public44 
async44 
Task44 
<44 
IList44 
<44  
string44  &
>44& '
>44' (
GetRoles44) 1
(441 2
string442 8
userId449 ?
)44? @
{55 	
return66 
await66 
_userManager66 %
.66% &
GetRolesAsync66& 3
(663 4
userId664 :
)66: ;
;66; <
}77 	
public99 
IList99 
<99 
IdentityRole99 !
>99! "
GetRoles99# +
(99+ ,
)99, -
{:: 	
return;; 
_roleManager;; 
.;;  
Roles;;  %
.;;% &
ToList;;& ,
(;;, -
);;- .
;;;. /
}<< 	
public>> 
void>> 
Dispose>> 
(>> 
)>> 
{?? 	
_ctx@@ 
.@@ 
Dispose@@ 
(@@ 
)@@ 
;@@ 
_userManagerAA 
.AA 
DisposeAA  
(AA  !
)AA! "
;AA" #
_roleManagerBB 
.BB 
DisposeBB  
(BB  !
)BB! "
;BB" #
}CC 	
}DD 
}EE !
C:\Users\matty\Documents\Development\Uni\Year 5\FYP\Uni-FYP-RestaurantBookingSystem\AuthService\Auth\SimpleAuthorizationServerProvider.cs
	namespace

 	
AuthService


 
.

 
Auth

 
{ 
public 

class -
!SimpleAuthorizationServerProvider 2
:3 4,
 OAuthAuthorizationServerProvider5 U
{ 
public 
override 
async 
Task "(
ValidateClientAuthentication# ?
(? @4
(OAuthValidateClientAuthenticationContext@ h
contexti p
)p q
{ 	
context 
. 
	Validated 
( 
) 
;  
} 	
public 
override 
async 
Task ")
GrantResourceOwnerCredentials# @
(@ A5
)OAuthGrantResourceOwnerCredentialsContextA j
contextk r
)r s
{ 	
context 
. 
OwinContext 
.  
Response  (
.( )
Headers) 0
.0 1
Add1 4
(4 5
$str5 R
,R S
newT W
[W X
]X Y
{Z [
$str\ _
}` a
)a b
;b c
var 
roles 
= 
new 
List  
<  !
string! '
>' (
(( )
)) *
;* +
string 
userId 
= 
$str 
; 
using 
( 
AuthRepository !
_repo" '
=( )
new* -
AuthRepository. <
(< =
)= >
)> ?
{ 
IdentityUser 
user !
=" #
await$ )
_repo* /
./ 0
FindUser0 8
(8 9
context9 @
.@ A
UserNameA I
,I J
contextK R
.R S
PasswordS [
)[ \
;\ ]
if 
( 
user 
== 
null  
)  !
{   
context!! 
.!! 
SetError!! $
(!!$ %
$str!!% 4
,!!4 5
$str!!6 _
)!!_ `
;!!` a
return"" 
;"" 
}## 
userId%% 
=%% 
user%% 
.%% 
Id%%  
;%%  !
roles&& 
=&& 
(&& 
(&& 
List&& 
<&& 
string&& %
>&&% &
)&&& '
await&&' ,
_repo&&- 2
.&&2 3
GetRoles&&3 ;
(&&; <
user&&< @
.&&@ A
Id&&A C
)&&C D
)&&D E
;&&E F
}'' 
var)) 
identity)) 
=)) 
new)) 
ClaimsIdentity)) -
())- .
context)). 5
.))5 6
Options))6 =
.))= >
AuthenticationType))> P
)))P Q
;))Q R
identity** 
.** 
AddClaim** 
(** 
new** !
Claim**" '
(**' (

ClaimTypes**( 2
.**2 3
Name**3 7
,**7 8
context**9 @
.**@ A
UserName**A I
)**I J
)**J K
;**K L
identity++ 
.++ 
AddClaim++ 
(++ 
new++ !
Claim++" '
(++' (
$str++( 0
,++0 1
userId++2 8
)++8 9
)++9 :
;++: ;
foreach-- 
(-- 
var-- 
role-- 
in--  
roles--! &
)--& '
{.. 
identity// 
.// 
AddClaim// !
(//! "
new//" %
Claim//& +
(//+ ,

ClaimTypes//, 6
.//6 7
Role//7 ;
,//; <
role//= A
)//A B
)//B C
;//C D
}00 
context22 
.22 
	Validated22 
(22 
identity22 &
)22& '
;22' (
}44 	
}55 
}66 ÚG
C:\Users\matty\Documents\Development\Uni\Year 5\FYP\Uni-FYP-RestaurantBookingSystem\AuthService\Controllers\AccountController.cs
	namespace 	
AuthService
 
. 
Controllers !
{ 
[ 
RoutePrefix 
( 
$str 
) 
]  
public 

class 
AccountController "
:# $
ApiController% 2
{ 
private 
AuthRepository 
_repo $
=% &
null' +
;+ ,
public 
AccountController  
(  !
)! "
{ 	
_repo 
= 
new 
AuthRepository &
(& '
)' (
;( )
} 	
[!! 	
AllowAnonymous!!	 
]!! 
["" 	
Route""	 
("" 
$str"" 
)"" 
]"" 
public## 
async## 
Task## 
<## 
IHttpActionResult## +
>##+ ,
Register##- 5
(##5 6
	UserModel##6 ?
	userModel##@ I
)##I J
{$$ 	
if%% 
(%% 
!%% 

ModelState%% 
.%% 
IsValid%% #
)%%# $
{&& 
return'' 

BadRequest'' !
(''! "

ModelState''" ,
)'', -
;''- .
}(( 
IdentityResult** 
result** !
=**" #
await**$ )
_repo*** /
.**/ 0
RegisterUser**0 <
(**< =
	userModel**= F
)**F G
;**G H
IHttpActionResult,, 
errorResult,, )
=,,* +
GetErrorResult,,, :
(,,: ;
result,,; A
),,A B
;,,B C
if.. 
(.. 
errorResult.. 
!=.. 
null.. #
)..# $
{// 
return00 
errorResult00 "
;00" #
}11 
return33 
Ok33 
(33 
)33 
;33 
}44 	
[66 	
HttpGet66	 
]66 
[77 	
	Authorize77	 
]77 
[88 	
Route88	 
(88 
$str88 
)88 
]88 
public99 
async99 
Task99 
<99 
IHttpActionResult99 +
>99+ ,
UserInfo99- 5
(995 6
)996 7
{:: 	
var;; 
identity;; 
=;; 
User;; 
.;;  
Identity;;  (
as;;) +
ClaimsIdentity;;, :
;;;: ;
var== 
id== 
=== 
identity== 
.== 
Claims== $
.==$ %
Where==% *
(==* +
c==+ ,
=>==- /
c==0 1
.==1 2
Type==2 6
====7 9
$str==: B
)==B C
.==C D
Select==D J
(==J K
c==K L
=>==M O
c==P Q
.==Q R
Value==R W
)==W X
.==X Y
FirstOrDefault==Y g
(==g h
)==h i
;==i j
if?? 
(?? 
id?? 
!=?? 
null?? 
)?? 
{@@ 
returnAA 
OkAA 
(AA 
newAA 
{AA 
UserIdAA  &
=AA' (
idAA) +
}AA, -
)AA- .
;AA. /
}BB 
elseCC 
{DD 
returnEE 
NotFoundEE 
(EE  
)EE  !
;EE! "
}FF 
}GG 	
[II 	
HttpGetII	 
]II 
[JJ 	
	AuthorizeJJ	 
]JJ 
[KK 	
RouteKK	 
(KK 
$strKK 
)KK 
]KK 
publicLL 
asyncLL 
TaskLL 
<LL 
IHttpActionResultLL +
>LL+ ,
IsAdminLL- 4
(LL4 5
)LL5 6
{MM 	
boolNN 
resNN 
=NN 
UserNN 
.NN 
IsInRoleNN $
(NN$ %
$strNN% ,
)NN, -
;NN- .
returnOO 
OkOO 
(OO 
resOO 
)OO 
;OO 
}PP 	
[RR 	
HttpGetRR	 
]RR 
[SS 	
	AuthorizeSS	 
]SS 
[TT 	
RouteTT	 
(TT 
$strTT 
)TT 
]TT 
publicUU 
asyncUU 
TaskUU 
<UU 
IHttpActionResultUU +
>UU+ ,
GetRolesUU- 5
(UU5 6
)UU6 7
{VV 	
varWW 
identityWW 
=WW 
UserWW 
.WW  
IdentityWW  (
asWW) +
ClaimsIdentityWW, :
;WW: ;
IEnumerableYY 
<YY 
stringYY 
>YY 
rolesYY  %
=YY& '
identityYY( 0
.YY0 1
ClaimsYY1 7
.YY7 8
WhereYY8 =
(YY= >
cYY> ?
=>YY@ B
cYYC D
.YYD E
TypeYYE I
==YYJ L

ClaimTypesYYM W
.YYW X
RoleYYX \
)YY\ ]
.YY] ^
SelectYY^ d
(YYd e
cYYe f
=>YYg i
cYYj k
.YYk l
ValueYYl q
)YYq r
;YYr s
return[[ 
Ok[[ 
([[ 
roles[[ 
)[[ 
;[[ 
}\\ 	
[^^ 	
	Authorize^^	 
]^^ 
[__ 	
Route__	 
(__ 
$str__  
)__  !
]__! "
public`` 
async`` 
Task`` 
<`` 
IHttpActionResult`` +
>``+ ,
AddCustomerRole``- <
(``< =
)``= >
{aa 	
varbb 
identitybb 
=bb 
Userbb 
.bb  
Identitybb  (
asbb) +
ClaimsIdentitybb, :
;bb: ;
vardd 
iddd 
=dd 
identitydd 
.dd 
Claimsdd $
.dd$ %
Wheredd% *
(dd* +
cdd+ ,
=>dd- /
cdd0 1
.dd1 2
Typedd2 6
==dd7 9
$strdd: B
)ddB C
.ddC D
SelectddD J
(ddJ K
cddK L
=>ddM O
cddP Q
.ddQ R
ValueddR W
)ddW X
.ddX Y
FirstOrDefaultddY g
(ddg h
)ddh i
;ddi j
ifff 
(ff 
idff 
!=ff 
nullff 
)ff 
{gg 
varhh 
reshh 
=hh 
awaithh 
_repohh  %
.hh% &

AssignRolehh& 0
(hh0 1
idhh1 3
,hh3 4
$strhh5 ?
)hh? @
;hh@ A
returnjj 
Okjj 
(jj 
)jj 
;jj 
}kk 
elsell 
{mm 
returnnn 
NotFoundnn 
(nn  
)nn  !
;nn! "
}oo 
}pp 	
	protectedrr 
overriderr 
voidrr 
Disposerr  '
(rr' (
boolrr( ,
	disposingrr- 6
)rr6 7
{ss 	
iftt 
(tt 
	disposingtt 
)tt 
{uu 
}ww 
baseyy 
.yy 
Disposeyy 
(yy 
	disposingyy "
)yy" #
;yy# $
}zz 	
private|| 
IHttpActionResult|| !
GetErrorResult||" 0
(||0 1
IdentityResult||1 ?
result||@ F
)||F G
{}} 	
if~~ 
(~~ 
result~~ 
==~~ 
null~~ 
)~~ 
{ 
return
 !
InternalServerError
 *
(
* +
)
+ ,
;
, -
}
 
if
 
(
 
!
 
result
 
.
 
	Succeeded
 !
)
! "
{
 
if
 
(
 
result
 
.
 
Errors
 !
!=
" $
null
% )
)
) *
{
 
foreach
 
(
 
string
 #
error
$ )
in
* ,
result
- 3
.
3 4
Errors
4 :
)
: ;
{
 

ModelState
 "
.
" #
AddModelError
# 0
(
0 1
$str
1 3
,
3 4
error
5 :
)
: ;
;
; <
}
 
}
 
if
 
(
 

ModelState
 
.
 
IsValid
 &
)
& '
{
 
return
 

BadRequest
 %
(
% &
)
& '
;
' (
}
 
return
 

BadRequest
 !
(
! "

ModelState
" ,
)
, -
;
- .
}
 
return
 
null
 
;
 
}
 	
}
 
} ú
}C:\Users\matty\Documents\Development\Uni\Year 5\FYP\Uni-FYP-RestaurantBookingSystem\AuthService\Controllers\AuthController.cs
	namespace 	
AuthService
 
. 
Controllers !
{ 
public 

class 
AuthController 
:  !
ApiController" /
{ 
private 
readonly 
AuthRepository '
	_authRepo( 1
;1 2
public 
AuthController 
( 
) 
{ 	
	_authRepo 
= 
new 
AuthRepository *
(* +
)+ ,
;, -
} 	
[ 	
AllowAnonymous	 
] 
[ 	
Route	 
( 
$str 
) 
] 
public 
IHttpActionResult  
GetRoles! )
() *
)* +
{ 	
IList 
< 
string 
> 
result  
=! "
	_authRepo# ,
., -
GetRoles- 5
(5 6
)6 7
.7 8
Select8 >
(> ?
r? @
=>A C
rD E
.E F
NameF J
)J K
.K L
ToListL R
(R S
)S T
;T U
return 
Ok 
( 
result 
) 
; 
} 	
} 
} Ü
sC:\Users\matty\Documents\Development\Uni\Year 5\FYP\Uni-FYP-RestaurantBookingSystem\AuthService\Data\AuthContext.cs
	namespace 	
AuthService
 
. 
Data 
{ 
public		 

class		 
AuthContext		 
:		 
IdentityDbContext		 0
<		0 1
IdentityUser		1 =
>		= >
{

 
public 
AuthContext 
( 
) 
: 
base 
( 
$str  
)  !
{ 	
} 	
} 
} Ď
sC:\Users\matty\Documents\Development\Uni\Year 5\FYP\Uni-FYP-RestaurantBookingSystem\AuthService\Models\UserModel.cs
	namespace 	
AuthService
 
. 
Models 
{ 
public		 

class		 
	UserModel		 
{

 
[ 	
Required	 
] 
[ 	
Display	 
( 
Name 
= 
$str #
)# $
]$ %
public 
string 
UserName 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Required	 
] 
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* Y
,Y Z
MinimumLength[ h
=i j
$numk l
)l m
]m n
[ 	
DataType	 
( 
DataType 
. 
Password #
)# $
]$ %
[ 	
Display	 
( 
Name 
= 
$str "
)" #
]# $
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
DataType	 
( 
DataType 
. 
Password #
)# $
]$ %
[ 	
Display	 
( 
Name 
= 
$str *
)* +
]+ ,
[ 	
Compare	 
( 
$str 
, 
ErrorMessage )
=* +
$str, b
)b c
]c d
public 
string 
ConfirmPassword %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} Ľ
zC:\Users\matty\Documents\Development\Uni\Year 5\FYP\Uni-FYP-RestaurantBookingSystem\AuthService\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str &
)& '
]' (
[		 
assembly		 	
:			 

AssemblyDescription		 
(		 
$str		 !
)		! "
]		" #
[

 
assembly

 	
:

	 
!
AssemblyConfiguration

  
(

  !
$str

! #
)

# $
]

$ %
[ 
assembly 	
:	 

AssemblyCompany 
( 
$str 
) 
] 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str (
)( )
]) *
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
["" 
assembly"" 	
:""	 

AssemblyVersion"" 
("" 
$str"" $
)""$ %
]""% &
[## 
assembly## 	
:##	 

AssemblyFileVersion## 
(## 
$str## (
)##( )
]##) *ą0
jC:\Users\matty\Documents\Development\Uni\Year 5\FYP\Uni-FYP-RestaurantBookingSystem\AuthService\Startup.cs
[ 
assembly 	
:	 

OwinStartup 
( 
typeof 
( 
AuthService )
.) *
Startup* 1
)1 2
)2 3
]3 4
	namespace 	
AuthService
 
{ 
public 

class 
Startup 
{ 
public 
void 
Configuration !
(! "
IAppBuilder" -
app. 1
)1 2
{ 	
ConfigureOAuth 
( 
app 
) 
;  
CreateRolesAndUsers 
(  
)  !
;! "
HttpConfiguration 
config $
=% &
new' *
HttpConfiguration+ <
(< =
)= >
;> ?
WebApiConfig 
. 
Register !
(! "
config" (
)( )
;) *
app 
. 
UseCors 
( 
	Microsoft !
.! "
Owin" &
.& '
Cors' +
.+ ,
CorsOptions, 7
.7 8
AllowAll8 @
)@ A
;A B
app 
. 
	UseWebApi 
( 
config  
)  !
;! "
} 	
public 
void 
ConfigureOAuth "
(" #
IAppBuilder# .
app/ 2
)2 3
{ 	+
OAuthAuthorizationServerOptions +
OAuthServerOptions, >
=? @
newA D+
OAuthAuthorizationServerOptionsE d
(d e
)e f
{   
AllowInsecureHttp!! !
=!!" #
true!!$ (
,!!( )
TokenEndpointPath"" !
=""" #
new""$ '

PathString""( 2
(""2 3
$str""3 ;
)""; <
,""< =%
AccessTokenExpireTimeSpan## )
=##* +
TimeSpan##, 4
.##4 5
FromDays##5 =
(##= >
$num##> ?
)##? @
,##@ A
Provider$$ 
=$$ 
new$$ -
!SimpleAuthorizationServerProvider$$ @
($$@ A
)$$A B
}%% 
;%% 
app(( 
.(( '
UseOAuthAuthorizationServer(( +
(((+ ,
OAuthServerOptions((, >
)((> ?
;((? @
app)) 
.)) (
UseOAuthBearerAuthentication)) ,
()), -
new))- 0,
 OAuthBearerAuthenticationOptions))1 Q
())Q R
)))R S
)))S T
;))T U
}++ 	
private-- 
void-- 
CreateRolesAndUsers-- (
(--( )
)--) *
{.. 	
AuthContext// 
context// 
=//  !
new//" %
AuthContext//& 1
(//1 2
)//2 3
;//3 4
var11 
roleManager11 
=11 
new11 !
RoleManager11" -
<11- .
IdentityRole11. :
>11: ;
(11; <
new11< ?
	RoleStore11@ I
<11I J
IdentityRole11J V
>11V W
(11W X
context11X _
)11_ `
)11` a
;11a b
var22 
UserManager22 
=22 
new22 !
UserManager22" -
<22- .
IdentityUser22. :
>22: ;
(22; <
new22< ?
	UserStore22@ I
<22I J
IdentityUser22J V
>22V W
(22W X
context22X _
)22_ `
)22` a
;22a b
if55 
(55 
!55 
roleManager55 
.55 

RoleExists55 '
(55' (
$str55( /
)55/ 0
)550 1
{66 
var99 
role99 
=99 
new99 
IdentityRole99 +
{:: 
Name;; 
=;; 
$str;; "
}<< 
;<< 
roleManager== 
.== 
Create== "
(==" #
role==# '
)==' (
;==( )
varAA 
userAA 
=AA 
newAA 
IdentityUserAA +
{BB 
UserNameCC 
=CC 
$strCC &
}DD 
;DD 
stringFF 
userPWDFF 
=FF  
$strFF! .
;FF. /
varHH 
chkUserHH 
=HH 
UserManagerHH )
.HH) *
CreateHH* 0
(HH0 1
userHH1 5
,HH5 6
userPWDHH7 >
)HH> ?
;HH? @
ifKK 
(KK 
chkUserKK 
.KK 
	SucceededKK %
)KK% &
{LL 
varMM 
result1MM 
=MM  !
UserManagerMM" -
.MM- .
	AddToRoleMM. 7
(MM7 8
userMM8 <
.MM< =
IdMM= ?
,MM? @
$strMMA H
)MMH I
;MMI J
}NN 
}OO 
ifRR 
(RR 
!RR 
roleManagerRR 
.RR 

RoleExistsRR '
(RR' (
$strRR( 1
)RR1 2
)RR2 3
{SS 
varTT 
roleTT 
=TT 
newTT 
IdentityRoleTT +
{UU 
NameVV 
=VV 
$strVV $
}WW 
;WW 
roleManagerXX 
.XX 
CreateXX "
(XX" #
roleXX# '
)XX' (
;XX( )
}YY 
if\\ 
(\\ 
!\\ 
roleManager\\ 
.\\ 

RoleExists\\ '
(\\' (
$str\\( 2
)\\2 3
)\\3 4
{]] 
var^^ 
role^^ 
=^^ 
new^^ 
IdentityRole^^ +
{__ 
Name`` 
=`` 
$str`` %
}aa 
;aa 
roleManagerbb 
.bb 
Createbb "
(bb" #
rolebb# '
)bb' (
;bb( )
}cc 
}dd 	
}ee 
}ff 