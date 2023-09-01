import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LangDialogComponent } from './lang-dialog.component';

describe('LangDialogComponent', () => {
  let component: LangDialogComponent;
  let fixture: ComponentFixture<LangDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LangDialogComponent]
    });
    fixture = TestBed.createComponent(LangDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
