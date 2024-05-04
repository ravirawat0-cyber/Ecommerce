import { ApplicationConfig } from '@angular/core';
import {provideRouter, withComponentInputBinding} from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {HttpClientModule} from "@angular/common/http";
import {importProvidersFrom} from "@angular/core";
import {AngularFireModule} from "@angular/fire/compat";





export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideClientHydration(), provideAnimationsAsync(), importProvidersFrom(HttpClientModule), provideRouter(routes, withComponentInputBinding())
  ,]
};
