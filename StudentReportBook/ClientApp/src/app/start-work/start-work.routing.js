"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var teacher_component_1 = require("./teacher/teacher.component");
var students_component_1 = require("./students/students.component");
var gradebook_component_1 = require("./gradebook/gradebook.component");
exports.routing = router_1.RouterModule.forChild([
    { path: 'teacher', component: teacher_component_1.TeacherComponent },
    { path: 'student', component: students_component_1.StudentsComponent },
    { path: 'gradebook', component: gradebook_component_1.GradeBookComponent }
]);
//# sourceMappingURL=start-work.routing.js.map