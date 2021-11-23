import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { OrderComponent } from './pages/admin/order/order.component';
import { OrderProgressComponent } from './pages/admin/order-progress/order-progress.component';
import { SubContractorsComponent } from './pages/admin/sub-contractors/sub-contractors.component';
import { ClientsComponent } from './pages/admin/clients/clients.component';
import { CommunicationComponent } from './pages/admin/communication/communication.component';
import { FooterComponent } from './shared/footer/footer.component';
import { ContainerComponent } from './shared/container/container.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { LayoutComponent } from './layout/layout.component';
import { AdminDashboardComponent } from './pages/admin/admin-dashboard/admin-dashboard.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AuthGuardService as AuthGuard } from './services/auth-guard.service';

const routes: Routes = [
  {
    path: "",
    redirectTo: "login",
    pathMatch: "full",
  },
    {path:"", component:LayoutComponent,children:[
      {path:"container",component:ContainerComponent},
      {path:"login",component:LoginComponent},
      {path:"admin-dashboard",component:AdminDashboardComponent,canActivate: [AuthGuard]},
      {path:"app-dashboard",component:DashboardComponent,canActivate: [AuthGuard]},
      {path:"app-footer",component:FooterComponent,canActivate: [AuthGuard]},
      {path:"communication",component:CommunicationComponent,canActivate: [AuthGuard]},
      {path:"clients",component:ClientsComponent,canActivate: [AuthGuard]},
      {path:"sub-contractors",component:SubContractorsComponent,canActivate: [AuthGuard]},
      {path:"order-progress",component:OrderProgressComponent,canActivate: [AuthGuard]},
      {path:"order",component:OrderComponent,canActivate: [AuthGuard]},
      {path:"forgot-password",component:ForgotPasswordComponent,}

    ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
