"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var http_1 = require("@angular/http");
var http_2 = require("@angular/common/http");
var config_service_1 = require("../utils/config.service");
var base_service_1 = require("./base.service");
var auth_service_1 = require("../../auth/auth.service");
var Rx_1 = require("rxjs/Rx");
// Add the RxJS Observable operators we need in this app.
require("../../rxjs-operators");
var UserService = /** @class */ (function (_super) {
    __extends(UserService, _super);
    function UserService(http, authService, httpClient, configService, baseUrl) {
        var _this = _super.call(this) || this;
        _this.http = http;
        _this.authService = authService;
        _this.httpClient = httpClient;
        _this.configService = configService;
        _this.baseUrls = '';
        //Observable navItem source
        _this.authNavStatusSource = new Rx_1.BehaviorSubject(false);
        //Observable navItem stream
        _this.authNavStatus$ = _this.authNavStatusSource.asObservable();
        _this.loggedIn = false;
        _this.loggedIn = authService.isAuthenticated();
        _this.authNavStatusSource.next(_this.loggedIn);
        _this.baseUrls = baseUrl;
        return _this;
    }
    UserService.prototype.register = function (email, password, firstName, lastName, role, department) {
        console.log(email + '' + password);
        var body = JSON.stringify({ email: email, password: password, firstName: firstName, lastName: lastName, role: role, department: department });
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        var options = new http_1.RequestOptions({ headers: headers });
        return this.http.post(this.baseUrls + "api/accounts", body, options)
            .map(function (res) { return true; })
            .catch(this.handleError);
    };
    UserService.prototype.login = function (userName, password) {
        var _this = this;
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        return this.http
            .post(this.baseUrls + 'api/auth/login', JSON.stringify({ userName: userName, password: password }), { headers: headers })
            .map(function (res) { return res.json(); })
            .map(function (res) {
            _this.loggedIn = _this.authService.setToken(res.auth_token);
            _this.authNavStatusSource.next(true);
            return true;
        })
            .catch(this.handleError);
    };
    UserService.prototype.logout = function () {
        localStorage.removeItem('auth_token');
        localStorage.removeItem('current_role');
        this.loggedIn = false;
        this.authNavStatusSource.next(false);
    };
    UserService = __decorate([
        core_1.Injectable(),
        __param(4, core_2.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.Http, auth_service_1.AuthService, http_2.HttpClient, config_service_1.ConfigService, String])
    ], UserService);
    return UserService;
}(base_service_1.BaseService));
exports.UserService = UserService;
//# sourceMappingURL=user.service.js.map