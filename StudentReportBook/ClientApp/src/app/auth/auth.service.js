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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var angular2_jwt_1 = require("angular2-jwt");
var decode_service_1 = require("../shared/services/decode.service");
var http_1 = require("../../../node_modules/@angular/common/http");
var AuthService = /** @class */ (function () {
    function AuthService(decodeService, httpClient, baseUrl) {
        this.decodeService = decodeService;
        this.httpClient = httpClient;
        this.baseUrls = '';
        this.baseUrls = baseUrl;
    }
    AuthService.prototype.getToken = function () {
        return localStorage.getItem('auth_token');
    };
    AuthService.prototype.setToken = function (token) {
        localStorage.setItem('auth_token', token);
        return true;
    };
    AuthService.prototype.getCurrentUser = function () {
        var userName = null;
        if (this.isAuthenticated()) {
            var token = this.getToken();
            userName = this.decodeService.getDecodedAccessToken(token).sub;
        }
        return userName;
    };
    AuthService.prototype.getCurrentUserRole = function () {
        var userRole = 'no role';
        if (this.isAuthenticated()) {
            if (localStorage.getItem('current_role') == null) {
                this.httpClient.get(this.baseUrls + 'api/auth/getCurrentRole').subscribe(function (result) {
                    localStorage.setItem('current_role', result);
                    userRole = localStorage.getItem('current_role');
                }, function (error) { return console.error(error); });
            }
            else {
                userRole = localStorage.getItem('current_role');
            }
        }
        return userRole;
    };
    AuthService.prototype.isAuthenticated = function () {
        // get the token
        var token = this.getToken();
        // return a boolean reflecting 
        // whether or not the token is expired
        return angular2_jwt_1.tokenNotExpired(null, token);
    };
    AuthService = __decorate([
        core_1.Injectable(),
        __param(2, core_1.Inject('BASE_URL')),
        __metadata("design:paramtypes", [decode_service_1.DecodeService, http_1.HttpClient, String])
    ], AuthService);
    return AuthService;
}());
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map