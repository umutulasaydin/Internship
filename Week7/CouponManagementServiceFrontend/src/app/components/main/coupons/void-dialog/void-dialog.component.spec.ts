import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VoidDialogComponent } from './void-dialog.component';

describe('VoidDialogComponent', () => {
  let component: VoidDialogComponent;
  let fixture: ComponentFixture<VoidDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [VoidDialogComponent]
    });
    fixture = TestBed.createComponent(VoidDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
