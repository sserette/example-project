import { ExampleProjectWebPage } from './app.po';

describe('example-project-web App', () => {
  let page: ExampleProjectWebPage;

  beforeEach(() => {
    page = new ExampleProjectWebPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
