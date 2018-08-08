"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var forms_1 = require("@angular/forms");
var shared_module_1 = require("../shared/modules/shared.module");
var auth_service_1 = require("../auth/auth.service");
var ngx_datatable_1 = require("@swimlane/ngx-datatable");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var user_service_1 = require("../shared/services/user.service");
var start_work_routing_1 = require("./start-work.routing");
var gradebook_component_1 = require("./gradebook/gradebook.component");
var moderator_component_1 = require("./moderator/moderator.component");
var teacher_component_1 = require("./teacher/teacher.component");
var start_work_component_1 = require("./start-work.component");
var StartWorkModule = /** @class */ (function () {
    function StartWorkModule() {
    }
    StartWorkModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule, forms_1.FormsModule, start_work_routing_1.routing, shared_module_1.SharedModule, ngx_datatable_1.NgxDatatableModule, ng_bootstrap_1.NgbCollapseModule.forRoot(), ng_bootstrap_1.NgbModule
            ],
            declarations: [teacher_component_1.TeacherComponent, moderator_component_1.ModeratorComponent, gradebook_component_1.GradeBookComponent, start_work_component_1.StartWorkComponent],
            providers: [user_service_1.UserService, auth_service_1.AuthService]
        })
    ], StartWorkModule);
    return StartWorkModule;
}());
exports.StartWorkModule = StartWorkModule;
//# sourceMappingURL=start-work.module.js.map