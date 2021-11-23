import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { LayoutModule } from './layout/layout.module';

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutComponent } from './layout/layout.component';
import { HeaderComponent } from './shared/header/header.component';
import { FooterComponent } from './shared/footer/footer.component';
import { ContainerComponent } from './shared/container/container.component';
import { SharedModule } from './shared/shared.module';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AdminDashboardComponent } from './pages/admin/admin-dashboard/admin-dashboard.component';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CommunicationComponent } from './pages/admin/communication/communication.component';
import { ClientsComponent } from './pages/admin/clients/clients.component';
import { SubContractorsComponent } from './pages/admin/sub-contractors/sub-contractors.component';
import { OrderProgressComponent } from './pages/admin/order-progress/order-progress.component';
import { OrderComponent } from './pages/admin/order/order.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialDesignModule } from './shared/material-design/material-design.module';
import { AvatarModule } from 'ngx-avatar';
import { ToastrModule } from 'ngx-toastr';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
// import { JwtHelperService } from '@auth0/angular-jwt';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    HeaderComponent,
    FooterComponent,
    ContainerComponent,
    DashboardComponent,
    AdminDashboardComponent,
    LoginComponent,
    CommunicationComponent,
    ClientsComponent,
    SubContractorsComponent,
    OrderProgressComponent,
    OrderComponent,
    ForgotPasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    LayoutModule,
    RouterModule,
    AvatarModule,
    HttpClientModule,
    MaterialDesignModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
