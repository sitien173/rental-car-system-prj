import {Component, OnInit} from '@angular/core';
import {OidcSecurityService} from "angular-auth-oidc-client";
import {environment} from "@ptit.rentalcar.app-config";
import {MenuItemModel} from "@syncfusion/ej2-angular-navigations";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isAuthenticated = false;
  profileMenu: MenuItemModel[];
  constructor(private readonly oidcSecurityService: OidcSecurityService) {
  }

  ngOnInit(): void {
    this.oidcSecurityService.checkAuth().subscribe((auth) =>
      this.isAuthenticated = auth.isAuthenticated
    );

    this.profileMenu = [
      {
        text: 'Profile',
        iconCss: 'e-icons e-profile',
        items: [
          {
            text: 'My Profile',
            iconCss: 'e-icons e-profile',
            url: '/profile',
          },
          {
            text: 'Sign Out',
            iconCss: 'e-icons e-logout',
            url: '/signout'
          }
        ]
      }
    ];
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
