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
var StudentsComponent = /** @class */ (function () {
    function StudentsComponent(http, baseUrl) {
        this.subjects = null;
        this.faculties = null;
        this.groups = null;
        this.students = null;
        this.editing = {};
        this.baseUrl = baseUrl;
        this.http = http;
    }
    StudentsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.http.get(this.baseUrl + 'api/teacher/getSubjectsForCurrentTeacher').subscribe(function (result) {
            _this.subjects = result;
            console.log(result);
        }, function (error) { return console.error(error); });
    };
    StudentsComponent.prototype.filterSubject = function (subjectId) {
        var _this = this;
        this.subjtId = subjectId;
        this.http.get(this.baseUrl + 'api/teacher/getFaculties', { params: { subjectId: subjectId } }).subscribe(function (result) {
            _this.faculties = result;
            console.log(result);
        }, function (error) { return console.error(error); });
    };
    StudentsComponent.prototype.filterFaculty = function (facId) {
        var _this = this;
        this.http.get(this.baseUrl + 'api/teacher/getGroups', { params: { subjectId: this.subjtId, facultyId: facId } }).subscribe(function (result) {
            _this.groups = result;
            console.log(result);
        }, function (error) { return console.error(error); });
    };
    StudentsComponent.prototype.filterGroup = function (groupId) {
        var _this = this;
        this.http.get(this.baseUrl + 'api/teacher/getStudents', { params: { groupId: groupId, subjectId: this.subjtId } }).subscribe(function (result) {
            _this.students = result;
            console.log(_this.students);
            if (!!_this.students) {
                var buf = new Array(_this.students[0].student.currentTerm);
                for (var i = 1; i <= _this.students[0].student.currentTerm; i++) {
                    buf[i - 1] = i;
                }
                _this.terms = buf;
            }
        }, function (error) { return console.error(error); });
    };
    StudentsComponent.prototype.updateValue = function (event, cell, rowIndex, term) {
        this.editing[rowIndex + '-' + cell + term] = false;
        console.log(this.students[rowIndex].student.id + ' ' + this.subjtId + ' ' + (term + 1) + ' ' + event.target.value);
        var body = {
            studentId: this.students[rowIndex].student.id,
            subjectId: this.subjtId,
            term: term + 1,
            grade: event.target.value
        };
        this.http.post(this.baseUrl + 'api/teacher/editMark', body)
            .subscribe(function (res) {
            if (!res) {
                alert("Cannot change grade");
            }
        });
    };
    StudentsComponent = __decorate([
        core_1.Component({
            selector: 'app-students',
            templateUrl: './students.component.html',
            styleUrls: ['./students.component.css']
        }),
        __param(1, core_1.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String])
    ], StudentsComponent);
    return StudentsComponent;
}());
exports.StudentsComponent = StudentsComponent;
//# sourceMappingURL=students.component.js.map