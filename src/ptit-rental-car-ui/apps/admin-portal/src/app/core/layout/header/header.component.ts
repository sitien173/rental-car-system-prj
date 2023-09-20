import {Component} from '@angular/core';
import {OidcSecurityService} from "angular-auth-oidc-client";
import {environment} from "@ptit.rentalcar.app-config";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  constructor(private readonly oidcSecurityService: OidcSecurityService,
              ) {
  }
  logout() {
    this.oidcSecurityService.logoffAndRevokeTokens().subscribe((result) => {
    });
  }

  profile() {
    window.open(environment.stsUri, '_blank');
  }
}
