"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var home_component_1 = require("./home/home.component");
var start_work_component_1 = require("./start-work/start-work.component");
var not_found_component_1 = require("./not-found/not-found.component");
var appRoutes = [
    { path: '', component: home_component_1.HomeComponent, pathMatch: 'full' },
    { path: '', component: home_component_1.HomeComponent },
    { path: 'start-work', component: start_work_component_1.StartWorkComponent },
    { path: '404', component: not_found_component_1.NotFoundComponent },
    { path: '**', redirectTo: '/404' },
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map