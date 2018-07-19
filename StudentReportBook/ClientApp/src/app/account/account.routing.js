"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var registration_form_component_1 = require("./registration-form/registration-form.component");
var login_form_component_1 = require("./login-form/login-form.component");
var home_component_1 = require("../home/home.component");
//import { FacebookLoginComponent } from './facebook-login/facebook-login.component';
exports.routing = router_1.RouterModule.forChild([
    { path: 'register', component: registration_form_component_1.RegistrationFormComponent },
    { path: 'login', component: login_form_component_1.LoginFormComponent },
    { path: 'logout', component: home_component_1.HomeComponent }
    // { path: 'facebook-login', component: FacebookLoginComponent }
]);
//# sourceMappingURL=account.routing.js.map