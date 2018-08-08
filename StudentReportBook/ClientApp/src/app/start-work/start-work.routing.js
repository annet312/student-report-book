"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var teacher_component_1 = require("./teacher/teacher.component");
var moderator_component_1 = require("./moderator/moderator.component");
var gradebook_component_1 = require("./gradebook/gradebook.component");
exports.routing = router_1.RouterModule.forChild([
    { path: 'moderator', component: moderator_component_1.ModeratorComponent },
    { path: 'teacher', component: teacher_component_1.TeacherComponent },
    { path: 'gradebook', component: gradebook_component_1.GradeBookComponent }
]);
//# sourceMappingURL=start-work.routing.js.map