import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {DOCUMENT} from "@angular/common";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  constructor(private readonly _renderer2: Renderer2,
              @Inject(DOCUMENT) private readonly _document: Document) {
  }

  ngOnInit(): void {
    const script = this._renderer2.createElement('script');
    script.type = `text/javascript`;
    script.src = `assets/js/main.js`;
    this._renderer2.appendChild(this._document.body, script);
  }
}
