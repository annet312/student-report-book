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
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var ModeratorComponent = /** @class */ (function () {
    function ModeratorComponent(http, baseUrl, modalService) {
        this.http = http;
        this.modalService = modalService;
        this.editing = {};
        this.baseUrl = baseUrl;
    }
    ModeratorComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.http.get(this.baseUrl + 'api/moderator/getTeachers').subscribe(function (result) {
            _this.teachers = result;
            console.log(result);
        }, function (error) { return console.error(error); });
    };
    ModeratorComponent.prototype.getContent = function (event) {
        var _this = this;
        if (event.nextId == "ngb-tab-1") {
            this.http.get(this.baseUrl + 'api/moderator/getStudentsWithoutGroup').subscribe(function (result) {
                _this.students = result;
                if (!_this.faculties) {
                    _this.http.get(_this.baseUrl + 'api/moderator/getAllFaculties').subscribe(function (res) {
                        _this.faculties = res;
                    }, function (error) { return console.error(error); });
                }
            }, function (error) { return console.error(error); });
        }
    };
    ModeratorComponent.prototype.setFaculty = function (facultyIndex) {
        this.groups = this.faculties[facultyIndex].groups;
    };
    ModeratorComponent.prototype.setGroup = function (studentId, rowIndex) {
        var _this = this;
        this.http.get(this.baseUrl + 'api/moderator/setGroupForStudent', { params: { studentId: studentId, groupId: this.selGroup.toArray()[rowIndex].nativeElement.value } })
            .subscribe(function (result) {
            if (result) {
                _this.editing[studentId] = !_this.editing[studentId];
                _this.students.splice(rowIndex, 1);
                _this.students = _this.students.slice();
            }
            else {
                alert("Can't set group");
            }
        }, function (error) { return console.error(error); });
    };
    ModeratorComponent.prototype.getTeacherWorkload = function (teacherId) {
        var _this = this;
        this.http.get(this.baseUrl + 'api/moderator/getTeacherWorkloads', { params: { teacherId: teacherId } })
            .subscribe(function (result) {
            _this.teacherWs = result;
            console.log(_this.teacherWs);
        });
    };
    ModeratorComponent.prototype.open = function (content, teacherId) {
        var _this = this;
        this.getTeacherWorkload(teacherId);
        this.modalService.open(content).result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reason) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reason);
        });
    };
    ModeratorComponent.prototype.getDismissReason = function (reason) {
        if (reason === ng_bootstrap_1.ModalDismissReasons.ESC) {
            return 'by pressing ESC';
        }
        else if (reason === ng_bootstrap_1.ModalDismissReasons.BACKDROP_CLICK) {
            return 'by clicking on a backdrop';
        }
        else {
            return "with: " + reason;
        }
    };
    __decorate([
        core_1.ViewChildren("selectGroup"),
        __metadata("design:type", core_1.QueryList)
    ], ModeratorComponent.prototype, "selGroup", void 0);
    ModeratorComponent = __decorate([
        core_1.Component({
            selector: 'app-moderator',
            templateUrl: './moderator.component.html'
        }),
        __param(1, core_1.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String, ng_bootstrap_1.NgbModal])
    ], ModeratorComponent);
    return ModeratorComponent;
}());
exports.ModeratorComponent = ModeratorComponent;
//# sourceMappingURL=moderator.component.js.map