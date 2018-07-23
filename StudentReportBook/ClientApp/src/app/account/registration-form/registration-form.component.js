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
var router_1 = require("@angular/router");
var user_service_1 = require("../../shared/services/user.service");
var RegistrationFormComponent = /** @class */ (function () {
    function RegistrationFormComponent(userService, router) {
        this.userService = userService;
        this.router = router;
        this.submitted = false;
    }
    RegistrationFormComponent.prototype.ngOnInit = function () {
    };
    RegistrationFormComponent.prototype.registerUser = function (_a) {
        var _this = this;
        var value = _a.value, valid = _a.valid;
        this.submitted = true;
        this.isRequesting = true;
        this.errors = '';
        if (valid) {
            console.log("register before calling");
            console.log(value);
            this.userService.register(value.email, value.password, value.firstName, value.lastName, value.role)
                .finally(function () {
                console.log("finally");
                _this.isRequesting = false;
            })
                .subscribe(function (result) {
                console.log(result);
                if (result) {
                    _this.router.navigate(['/login'], { queryParams: { brandNew: true, email: value.email } });
                }
                else {
                    console.log("result is false");
                }
            }, function (errors) {
                _this.errors = errors;
                console.log("errors:");
                console.log(_this.errors);
            });
        }
    };
    RegistrationFormComponent = __decorate([
        core_1.Component({
            selector: 'app-registration-form',
            templateUrl: './registration-form.component.html',
            styleUrls: ['./registration-form.component.css']
        }),
        __metadata("design:paramtypes", [user_service_1.UserService, router_1.Router])
    ], RegistrationFormComponent);
    return RegistrationFormComponent;
}());
exports.RegistrationFormComponent = RegistrationFormComponent;
//# sourceMappingURL=registration-form.component.js.map