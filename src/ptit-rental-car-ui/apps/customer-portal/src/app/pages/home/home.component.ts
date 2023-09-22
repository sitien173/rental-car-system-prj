import {AfterViewInit, Component, OnInit} from '@angular/core';
import {OidcSecurityService} from "angular-auth-oidc-client";
declare const $: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {
  isAuthorized: boolean;
  constructor(private readonly _oidcSecurityService: OidcSecurityService) {
  }

  ngOnInit(): void {
    this._oidcSecurityService.isAuthenticated$.subscribe(({isAuthenticated}) => {
      this.isAuthorized = isAuthenticated;
    });
  }

  ngAfterViewInit(): void {
    $('.owl-carousel').owlCarousel({
      loop: true,
      margin: 10,
      nav: true,
      items: 3
    });
  }
}
