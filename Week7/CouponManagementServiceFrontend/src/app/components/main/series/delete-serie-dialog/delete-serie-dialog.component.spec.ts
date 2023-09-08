import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteSerieDialogComponent } from './delete-serie-dialog.component';

describe('DeleteSerieDialogComponent', () => {
  let component: DeleteSerieDialogComponent;
  let fixture: ComponentFixture<DeleteSerieDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteSerieDialogComponent]
    });
    fixture = TestBed.createComponent(DeleteSerieDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
