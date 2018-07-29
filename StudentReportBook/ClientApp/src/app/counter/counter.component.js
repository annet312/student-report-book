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
var http_1 = require("@angular/common/http");
var CounterComponent = /** @class */ (function () {
    function CounterComponent(http, baseUrl) {
        var _this = this;
        this.row = [];
        this.rowmarks = [];
        this.groups = [];
        http.get(baseUrl + 'api/student/getMygradeBook').subscribe(function (result) {
            _this.gradebook = result;
            console.log(result);
            _this.row = [{
                    name: _this.gradebook.student.name,
                    studentCard: _this.gradebook.student.studentCard,
                    currentTerm: _this.gradebook.student.currentTerm,
                    group: _this.gradebook.student.group,
                    faculty: _this.gradebook.student.faculty
                }];
            _this.rowmarks = _this.gradebook.marks;
        }, function (error) { return console.error(error); });
    }
    CounterComponent.prototype.ngOnInit = function () {
    };
    CounterComponent.prototype.getGroupRowHeight = function (group, rowHeight) {
        var style = {};
        style = {
            height: (group.length * 40) + 'px',
            width: '100%'
        };
        return style;
    };
    CounterComponent.prototype.ToggleExpandGroup = function (group) {
        console.log('onToggleExpandGroup', group);
        this.markTable.groupHeader.toggleExpandGroup(group);
    };
    CounterComponent.prototype.onDetailToggle = function (event) {
        console.log('onDetailToggle', event);
    };
    __decorate([
        core_1.ViewChild('markTable'),
        __metadata("design:type", Object)
    ], CounterComponent.prototype, "markTable", void 0);
    CounterComponent = __decorate([
        core_1.Component({
            selector: 'app-counter-component',
            templateUrl: './counter.component.html'
        }),
        __param(1, core_1.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String])
    ], CounterComponent);
    return CounterComponent;
}());
exports.CounterComponent = CounterComponent;
//# sourceMappingURL=counter.component.js.map