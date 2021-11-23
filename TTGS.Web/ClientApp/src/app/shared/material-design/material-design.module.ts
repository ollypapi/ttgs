import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatCardModule} from '@angular/material/card';
import{MatButtonModule} from "@angular/material/button";
import {MatToolbarModule} from '@angular/material/toolbar';
import { MatIconModule} from "@angular/material/icon";

const MaterialComponents = [
  MatCardModule,
  MatIconModule,
  MatButtonModule,MatToolbarModule
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MaterialComponents
  ],exports:[MaterialComponents]
})
export class MaterialDesignModule { }
