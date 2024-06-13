#import "AdGemOfferwallDelegate.h"

@implementation AdGemOfferwallDelegate

- (void) offerwallLoadingStarted {
    _onOfferwallLoadingStarted();
}

- (void) offerwallLoadingFinished {
    _onOfferwallLoadingFinished();
}

- (void) offerwallLoadingFailedWithError:(NSError*) error {
    _onOfferwallLoadingFailedWithError(error.localizedDescription);
}

- (void) offerwallRewardReceivedWithAmount:(NSInteger) amount {
    _onOfferwallRewardReceived(amount);
}

- (void) offerwallClosed {
    _onOfferwallClosed();
}

@end

