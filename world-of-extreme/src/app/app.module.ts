import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { Configuration } from './app.constants';
import { routing } from './app.routes';

import { HttpClient, HttpClientModule } from '@angular/common/http';
import { SecureFileService } from './securefile/SecureFileService';

import { ForbiddenComponent } from './forbidden/forbidden.component';
import { HomeComponent } from './home/home.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { SecureFilesComponent } from './securefile/securefiles.component';

import { AuthModule, OidcConfigService } from './auth/angular-auth-oidc-client';

import { AuthorizationGuard } from './authorization.guard';

export function configureAuth(oidcConfigService: OidcConfigService) {
    return () =>
        oidcConfigService.withConfig({
            stsServer: 'https://localhost:51786',
            redirectUrl: 'https://localhost:4200',
            clientId: 'worldofextremeclient',
            responseType: 'id_token',
            scope: 'openid profile email',
            postLogoutRedirectUri: 'https://localhost:4200/Unauthorized',
            startCheckSession: false,
            silentRenew: true,
            silentRenewUrl: 'https://localhost:4200/silent-renew.html',
            postLoginRoute: '/home',
            forbiddenRoute: '/Forbidden',
            unauthorizedRoute: '/Unauthorized',
            logLevel: 0, // LogLevel.Debug, 
            autoCleanStateAfterAuthentication: false
            // autoUserinfo: false,
        });
}

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        routing,
        HttpClientModule,
        AuthModule.forRoot(),
    ],
    declarations: [
        AppComponent,
        ForbiddenComponent,
        HomeComponent,
        UnauthorizedComponent,
        SecureFilesComponent
    ],
    providers: [
        OidcConfigService,
        {
            provide: APP_INITIALIZER,
            useFactory: configureAuth,
            deps: [OidcConfigService, HttpClient],
            multi: true,
        },
        AuthorizationGuard,
        SecureFileService,
        Configuration
    ],
    bootstrap: [AppComponent],
})

export class AppModule {

    constructor() {
              console.log('APP STARTING');
    }
}
