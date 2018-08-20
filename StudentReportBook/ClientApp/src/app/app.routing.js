"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var home_component_1 = require("./home/home.component");
var gradebook_component_1 = require("./start-work/gradebook/gradebook.component");
var not_found_component_1 = require("./not-found/not-found.component");
var role_guard_1 = require("./role.guard");
var teacher_component_1 = require("./start-work/teacher/teacher.component");
var moderator_component_1 = require("./start-work/moderator/moderator.component");
var start_work_component_1 = require("./start-work/start-work.component");
var appRoutes = [
    { path: '', component: home_component_1.HomeComponent, pathMatch: 'full' },
    { path: '', component: home_component_1.HomeComponent },
    {
        path: 'start-work', component: start_work_component_1.StartWorkComponent
    },
    {
        path: 'teacher', component: teacher_component_1.TeacherComponent, canActivate: [role_guard_1.RoleGuardService],
        data: {
            expectedRole: 'Teacher'
        }
    },
    {
        path: 'gradebook', component: gradebook_component_1.GradeBookComponent, canActivate: [role_guard_1.RoleGuardService],
        data: {
            expectedRole: 'Student'
        }
    },
    {
        path: 'moderator', component: moderator_component_1.ModeratorComponent, canActivate: [role_guard_1.RoleGuardService],
        data: {
            expectedRole: 'Moderator'
        }
    },
    { path: '404', component: not_found_component_1.NotFoundComponent },
    { path: '**', redirectTo: '/404' },
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map