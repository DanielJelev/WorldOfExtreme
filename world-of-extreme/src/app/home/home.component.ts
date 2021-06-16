import {
    OidcClientNotification,
    OidcSecurityService,
} from '../auth/angular-auth-oidc-client';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
    selector: 'app-home',
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {

    message: string;
    name = 'none';
    email = 'none';

    userDataChanged$: Observable<OidcClientNotification<any>>;
    isAuthenticated$: Observable<boolean>;
    private headers: HttpHeaders = new HttpHeaders();
    constructor(public http: HttpClient, public oidcSecurityService: OidcSecurityService) {

    }

    ngOnInit() {
        this.isAuthenticated$ = this.oidcSecurityService.isAuthenticated$;

        this.oidcSecurityService.userData$
            .subscribe((userData) => {
                if (userData) {
                    this.name = userData.name;
                  this.email = userData.email;
                  this.asd();
                }
            });


     

  }

  asd() {
    var headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json');
    headers = headers.set('Accept', 'application/json');

    //const token = `${this.oidcSecurityService.getIdToken()} ${this.oidcSecurityService.getToken()}`;
    const token = this.oidcSecurityService.getIdToken();
    if (token !== '') {
      const tokenValue = 'Bearer ' + token;
      headers = headers.set('Authorization', tokenValue);
    }
    this.http.get("https://localhost:44372/profile", { headers: headers }).subscribe((data) => {
      console.log(data)
    })
  }
}
