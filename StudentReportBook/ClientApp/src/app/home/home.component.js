"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var user_service_1 = require("../shared/services/user.service");
var auth_service_1 = require("../auth/auth.service");
var HomeComponent = /** @class */ (function () {
    function HomeComponent(auth, userService) {
        this.auth = auth;
        this.userService = userService;
        this.name = null;
        this.IsLoggedIn = false;
    }
    HomeComponent.prototype.ngOnInit = function () {
        this.IsLoggedIn = this.auth.isAuthenticated();
        if (this.IsLoggedIn) {
            this.name = this.auth.getCurrentUser();
            console.log(this.auth.getCurrentUserRole());
        }
    };
    HomeComponent.prototype.submitlogout = function () {
        this.name = null;
        this.userService.logout();
        this.IsLoggedIn = false;
    };
    HomeComponent = __decorate([
        core_1.Component({
            selector: 'app-home',
            templateUrl: './home.component.html',
            styleUrls: ['./home.component.scss']
        }),
        __metadata("design:paramtypes", [auth_service_1.AuthService, user_service_1.UserService])
    ], HomeComponent);
    return HomeComponent;
}());
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.component.js.map