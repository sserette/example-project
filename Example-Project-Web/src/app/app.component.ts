import { Component, ViewContainerRef, ChangeDetectorRef, OnInit } from '@angular/core';

import { Configuration } from './app.constants';
import { BusyService } from './core/index';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  busy: boolean;
  title = 'Example Application';

  constructor(
    public configuration: Configuration,
    public viewContainerRef: ViewContainerRef,
    public busyService: BusyService,
    public changeDetectorRef: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.busyService.busyEvent.subscribe(busy => {
      this.busy = busy;
    });
  }
}
