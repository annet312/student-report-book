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
var router_1 = require("@angular/router");
var StartWorkComponent = /** @class */ (function () {
    function StartWorkComponent(userService, router, auth) {
        this.userService = userService;
        this.router = router;
        this.auth = auth;
        this.title = 'start-work';
    }
    StartWorkComponent.prototype.ngOnInit = function () {
        var role = this.auth.getCurrentUserRole();
        switch (role) {
            case "Student": {
                this.router.navigate(['gradebook']);
                break;
            }
            case "Teacher": {
                this.router.navigate(['teacher']);
                break;
            }
            case "Moderator": {
                this.router.navigate(['moderator']);
                break;
            }
            default: {
                this.router.navigate(['/']);
                break;
            }
        }
    };
    StartWorkComponent = __decorate([
        core_1.Component({
            selector: 'start-work',
            templateUrl: './start-work.component.html',
        }),
        __metadata("design:paramtypes", [user_service_1.UserService, router_1.Router, auth_service_1.AuthService])
    ], StartWorkComponent);
    return StartWorkComponent;
}());
exports.StartWorkComponent = StartWorkComponent;
//# sourceMappingURL=start-work.component.js.map