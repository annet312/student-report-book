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
var config_service_1 = require("../utils/config.service");
var decode_service_1 = require("./decode.service");
var base_service_1 = require("./base.service");
var Rx_1 = require("rxjs/Rx");
//import { UserResponse } from '../models/UserResponse';
// Add the RxJS Observable operators we need in this app.
require("../../rxjs-operators");
var UserService = /** @class */ (function (_super) {
    __extends(UserService, _super);
    function UserService(http, configService, baseUrl, decodeService) {
        var _this = _super.call(this) || this;
        _this.http = http;
        _this.configService = configService;
        _this.decodeService = decodeService;
        _this.baseUrls = '';
        //Observable navItem source
        _this.authNavStatusSource = new Rx_1.BehaviorSubject(false);
        //Observable navItem stream
        _this.authNavStatus$ = _this.authNavStatusSource.asObservable();
        _this.loggedIn = false;
        _this.loggedIn = !!localStorage.getItem('auth_token');
        _this.authNavStatusSource.next(_this.loggedIn);
        _this.baseUrls = baseUrl;
        return _this;
        //this.baseUrl = configService.getApiURI();
    }
    UserService.prototype.register = function (email, password, firstName, lastName, role) {
        console.log("in registration");
        console.log(email + '' + password);
        var body = JSON.stringify({ email: email, password: password, firstName: firstName, lastName: lastName, role: role });
        console.log("body");
        console.log(body);
        var headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        console.log(headers);
        var options = new http_1.RequestOptions({ headers: headers });
        console.log(options);
        console.log(this.baseUrls);
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
            localStorage.setItem('auth_token', res.auth_token);
            console.log(res.auth_token);
            _this.loggedIn = true;
            _this.authNavStatusSource.next(true);
            return true;
        })
            .catch(this.handleError);
    };
    UserService.prototype.getCurrentUser = function () {
        var userName = null;
        if (this.loggedIn) {
            var token = localStorage.getItem('auth_token');
            userName = this.decodeService.getDecodedAccessToken(token).sub;
        }
        return userName;
    };
    UserService.prototype.logout = function () {
        localStorage.removeItem('auth_token');
        this.loggedIn = false;
        this.authNavStatusSource.next(false);
    };
    UserService.prototype.isLoggedIn = function () {
        return this.loggedIn;
    };
    UserService = __decorate([
        core_1.Injectable(),
        __param(2, core_2.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.Http, config_service_1.ConfigService, String, decode_service_1.DecodeService])
    ], UserService);
    return UserService;
}(base_service_1.BaseService));
exports.UserService = UserService;
//# sourceMappingURL=user.service.js.map