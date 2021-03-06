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
var teacher_workload_add_class_1 = require("../../../shared/models/teacher-workload.add.class");
var ModerateTeacherComponent = /** @class */ (function () {
    function ModerateTeacherComponent(http, baseUrl, modalService) {
        this.http = http;
        this.modalService = modalService;
        this.editing = {};
        this.IfAdding = false;
        this.submitted = false;
        this.baseUrl = baseUrl;
    }
    ModerateTeacherComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.http.get(this.baseUrl + 'api/moderator/getTeachers').subscribe(function (result) {
            _this.teachers = result;
        }, function (error) { return console.error(error); });
    };
    ModerateTeacherComponent.prototype.getTeacherWorkload = function (teacherId) {
        var _this = this;
        this.http.get(this.baseUrl + 'api/moderator/getTeacherWorkloads', { params: { teacherId: teacherId } })
            .subscribe(function (result) {
            _this.teacherWs = result;
            console.log(_this.teacherWs);
        });
    };
    ModerateTeacherComponent.prototype.getAllSubjects = function () {
        var _this = this;
        if (!this.subjects) {
            this.http.get(this.baseUrl + 'api/moderator/getAllSubjects').subscribe(function (result) {
                _this.subjects = result;
                console.log(result);
            }, function (error) { return console.error(error); });
        }
    };
    ModerateTeacherComponent.prototype.updateSubject = function (event, teacherWorkloadId, rowIndex) {
        var _this = this;
        this.http.get(this.baseUrl + 'api/moderator/changeSubject', { params: { teacherWorkloadId: teacherWorkloadId, subjectId: event.target.value } })
            .subscribe(function (result) {
            if (result) {
                _this.editing[rowIndex + '-subject'] = false;
                _this.teacherWs[rowIndex].subject = {
                    id: event.target.value,
                    name: event.target.selectedOptions[0].innerText
                };
            }
            else {
                alert("Can't set subject");
            }
        }, function (error) {
            console.error(error.error);
            alert("Can't set subject");
        });
    };
    ModerateTeacherComponent.prototype.updateTerm = function (event, teacherWorkloadId, rowIndex) {
        var _this = this;
        console.log(event);
        this.http.get(this.baseUrl + 'api/moderator/changeTerm', { params: { teacherWorkloadId: teacherWorkloadId, term: event.target.value } })
            .subscribe(function (result) {
            if (result) {
                _this.editing[rowIndex + '-term'] = false;
                _this.teacherWs[rowIndex].term = event.target.value;
            }
            else {
                alert("Can't set term");
            }
        }, function (error) {
            console.error(error.error);
            alert("Can't set term.");
        });
    };
    ModerateTeacherComponent.prototype.updateGroup = function (event, teacherWorkloadId, rowIndex) {
        var _this = this;
        console.log(event);
        this.http.get(this.baseUrl + 'api/moderator/changeGroup', { params: { teacherWorkloadId: teacherWorkloadId, groupId: event.target.value } })
            .subscribe(function (result) {
            if (result) {
                _this.editing[rowIndex + '-group'] = false;
                _this.teacherWs[rowIndex].group.id = event.target.value;
                console.log(event.target.selectedOptions[0].innerText);
                _this.teacherWs[rowIndex].group.name = event.target.selectedOptions[0].innerText;
            }
            else {
                alert("Can't set group");
            }
        }, function (error) {
            console.error(error.error);
            alert("Can't set group.");
        });
    };
    ModerateTeacherComponent.prototype.getAllGroups = function () {
        var _this = this;
        if (!this.listOfGroups) {
            this.http.get(this.baseUrl + 'api/moderator/getAllGroups').subscribe(function (res) {
                _this.listOfGroups = res;
            }, function (error) { return console.error(error); });
        }
        console.log(this.listOfGroups);
    };
    ModerateTeacherComponent.prototype.open = function (content, teacherId) {
        var _this = this;
        this.getTeacherWorkload(teacherId);
        this.modalService.open(content, { size: 'lg', backdrop: 'static' }).result.then(function (result) {
            _this.closeResult = "Closed with: " + result;
        }, function (reason) {
            _this.closeResult = "Dismissed " + _this.getDismissReason(reason);
        });
    };
    ModerateTeacherComponent.prototype.openAddWorload = function (content, teacherId) {
        this.IfAdding = true;
        this.getAllSubjects();
        this.getAllGroups();
    };
    ModerateTeacherComponent.prototype.submitNewWorkload = function (_a, teacherId) {
        var _this = this;
        var value = _a.value, valid = _a.valid;
        this.IfAdding = false;
        this.submitted = true;
        this.errors = '';
        if (valid) {
            var newTW = new teacher_workload_add_class_1.TeacherWorkloadAdd(value.subjectId, value.groupId, value.term, teacherId);
            this.http.post(this.baseUrl + 'api/moderator/addWorkload', newTW)
                .subscribe(function (data) {
                console.log('success', data);
                if (data == null) {
                    alert("Cannot add workload");
                }
                else {
                    _this.teacherWs.push(data);
                    _this.teacherWs = _this.teacherWs.slice();
                }
            }, function (error) {
                console.log('oops', error.error);
                alert("Cannot add workload");
            });
        }
    };
    ModerateTeacherComponent.prototype.delete = function (rowIndex) {
        var _this = this;
        if (window.confirm('Are sure you want to delete this item ?')) {
            this.http.delete(this.baseUrl + 'api/moderator/deleteWorkload/' + this.teacherWs[rowIndex].id)
                .subscribe(function (res) {
                console.log("workload " + _this.teacherWs[rowIndex].id + " was deleting");
                _this.teacherWs.splice(rowIndex, 1);
            }, function (error) {
                console.error(error.error);
                alert("Cannot delete");
            });
        }
    };
    ModerateTeacherComponent.prototype.closeAddWorkload = function () {
        this.IfAdding = false;
    };
    ModerateTeacherComponent.prototype.getDismissReason = function (reason) {
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
    ModerateTeacherComponent = __decorate([
        core_1.Component({
            selector: 'app-moderate-teacher',
            templateUrl: './moderate-teacher.component.html',
            styleUrls: ['./moderate-teacher.component.css']
        }),
        __param(1, core_1.Inject('BASE_URL')),
        __metadata("design:paramtypes", [http_1.HttpClient, String, ng_bootstrap_1.NgbModal])
    ], ModerateTeacherComponent);
    return ModerateTeacherComponent;
}());
exports.ModerateTeacherComponent = ModerateTeacherComponent;
//# sourceMappingURL=moderate-teacher.component.js.map