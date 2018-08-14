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
var ng_bootstrap_1 = require("../../../../../node_modules/@ng-bootstrap/ng-bootstrap");
var ModerateStudentComponent = /** @class */ (function () {
    function ModerateStudentComponent(http, baseUrl, modalService) {
        this.http = http;
        this.modalService = modalService;
        this.editing = {};
        this.errorString = {};
        this.baseUrl = baseUrl;
    }
    ModerateStudentComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.http.get(this.baseUrl + 'api/moderator/getStudentsWithoutGroup').subscribe(function (result) {
            _this.students = result;
            if (!_this.faculties) {
                _this.http.get(_this.baseUrl + 'api/moderator/getAllFaculties').subscribe(function (res) {
                    _this.faculties = res;
                }, function (error) { return console.error(error); });
            }
        }, function (error) { return console.error(error); });
    };
    ModerateStudentComponent.prototype.setFaculty = function (facultyIndex) {
        this.groups = this.faculties[facultyIndex].groups;
    };
    ModerateStudentComponent.prototype.checkStudentCard = function (event, studentId) {
        console.log(event.target.value);
        if ((event.target.value > 10000) && (event.target.value < 100000)) {
            this.editing[studentId + '-studentcard'] = true;
            this.errorString[studentId] = null;
        }
        else {
            this.errorString[studentId] = 'Student card must be in range from 10000 to 99999';
        }
    };
    ModerateStudentComponent.prototype.setGroup = function (studentId, rowIndex) {
        var _this = this;
        var stCard = this.studentCard.toArray()[rowIndex].nativeElement.value;
        console.log(stCard);
        this.http.get(this.baseUrl + 'api/moderator/setGroupForStudent', { params: { studentId: studentId, groupId: this.selGroup.toArray()[rowIndex].nativeElement.value, studentCard: stCard } })
            .subscribe(function (result) {
            if (result) {
                _this.students.splice(rowIndex, 1);
            }
            else {
                alert("Can't set group");
            }
        }, function (error) {
            console.error(error);
            alert("Can't set group or student card: " + error.error);
        });
    };
    __decorate([
        core_1.ViewChildren("selectGroup"),
        __metadata("design:type", core_1.QueryList)
    ], ModerateStudentComponent.prototype, "selGroup", void 0);
    __decorate([
        core_1.ViewChildren("studentCardInput"),
        __metadata("design:type", core_1.QueryList)
    ], ModerateStudentComponent.prototype, "studentCard", void 0);
    ModerateStudentComponent = __decorate([
        core_1.Component({
            selector: 'app-moderate-student',
            templateUrl: './moderate-student.component.html',
            styleUrls: ['./moderate-student.component.css']
        }),
        __param(1, core_1.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String, ng_bootstrap_1.NgbModal])
    ], ModerateStudentComponent);
    return ModerateStudentComponent;
}());
exports.ModerateStudentComponent = ModerateStudentComponent;
//# sourceMappingURL=moderate-student.component.js.map