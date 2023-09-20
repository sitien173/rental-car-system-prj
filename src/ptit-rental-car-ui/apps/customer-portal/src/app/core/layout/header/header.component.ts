import {Component, OnInit} from '@angular/core';
import {OidcSecurityService} from "angular-auth-oidc-client";
import {environment} from "@ptit.rentalcar.app-config";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isAuthenticated = false;
  constructor(private readonly oidcSecurityService: OidcSecurityService) {
  }

  ngOnInit(): void {
    this.oidcSecurityService.checkAuth().subscribe((auth) =>
      this.isAuthenticated = auth.isAuthenticated
    );
  }

  userInfo() {
    window.open(environment.stsUri, '_blank');
  }

  signOut() {
    this.oidcSecurityService.logoffAndRevokeTokens()
      .subscribe((result) => {
        console.log(result);
    });
  }

  signIn() {
    this.oidcSecurityService.authorize();
    this.oidcSecurityService.checkAuth().subscribe((auth) =>
      this.isAuthenticated = auth.isAuthenticated
    );
  }
}
