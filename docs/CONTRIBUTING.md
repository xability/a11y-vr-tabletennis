# Contributing to VR Table Tennis

Thank you for your interest in contributing to the VR Table Tennis project! This guide will help you understand how to contribute effectively while maintaining the project's accessibility focus and code quality.

## Table of Contents

- [Getting Started](#getting-started)
- [Development Setup](#development-setup)
- [Code Standards](#code-standards)
- [Accessibility Guidelines](#accessibility-guidelines)
- [Testing Procedures](#testing-procedures)
- [Pull Request Process](#pull-request-process)
- [Issue Reporting](#issue-reporting)

## Getting Started

### Prerequisites

- Unity 2022.3 LTS or later
- Meta Quest 2/3 or compatible VR headset
- Basic understanding of C# and Unity
- Familiarity with accessibility concepts (we'll help you learn!)

### Development Environment

1. **Fork the Repository**
   ```bash
   git clone https://github.com/xability/a11y-vr-tabletennis.git
   cd vr_tt
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Open the project folder
   - Wait for Unity to import all assets

3. **Configure VR Settings**
   - Go to `Edit > Project Settings > XR Plug-in Management`
   - Enable "Oculus" and "OpenXR" plugins
   - Configure your VR headset settings

## Development Setup

### Project Structure

The project follows a modular architecture:

```
Assets/Scripts/
├── Core/           # Core game management
├── Gameplay/       # Game mechanics
├── Interaction/    # VR interactions
├── Accessibility/  # BLV accessibility features
└── UI/            # User interface
```

### Creating New Features

1. **Choose the Right Module**
   - Core: Game management and coordination
   - Gameplay: Ball physics, scoring, game mechanics
   - Interaction: VR input, hand tracking, object manipulation
   - Accessibility: Audio cues, haptic feedback, BLV features
   - UI: Menus, settings, user interface

2. **Follow Naming Conventions**
   ```csharp
   // Class names: PascalCase
   public class NewFeature : MonoBehaviour
   
   // Method names: PascalCase
   public void HandleUserInput()
   
   // Variable names: camelCase
   private float ballSpeed = 10f;
   
   // Constants: UPPER_CASE
   private const float MAX_BALL_SPEED = 20f;
   ```

3. **Use Proper Namespaces**
   ```csharp
   namespace VRTableTennis.Gameplay
   {
       public class NewGameplayFeature : MonoBehaviour
       {
           // Implementation
       }
   }
   ```

## Code Standards

### C# Coding Standards

1. **Documentation**
   ```csharp
   /// <summary>
   /// Handles ball collision with the paddle and provides haptic feedback
   /// </summary>
   /// <param name="collision">The collision data</param>
   public void OnPaddleCollision(Collision collision)
   {
       // Implementation
   }
   ```

2. **Error Handling**
   ```csharp
   private void InitializeComponent()
   {
       if (component == null)
       {
           Debug.LogError("Component not found. Please check the setup.");
           return;
       }
       // Continue initialization
   }
   ```

3. **Performance Considerations**
   ```csharp
   // Cache frequently accessed components
   private AudioSource audioSource;
   
   void Awake()
   {
       audioSource = GetComponent<AudioSource>();
   }
   ```

### Unity-Specific Standards

1. **Component Organization**
   ```csharp
   [Header("Settings")]
   public float speed = 10f;
   
   [Header("References")]
   public GameObject target;
   
   [Header("Debug")]
   [SerializeField] private bool showDebugInfo = false;
   ```

2. **Event Handling**
   ```csharp
   private void OnEnable()
   {
       GameManager.Instance.OnGameStart += HandleGameStart;
   }
   
   private void OnDisable()
   {
       GameManager.Instance.OnGameStart -= HandleGameStart;
   }
   ```

## Accessibility Guidelines

### Core Principles

1. **Multiple Feedback Modalities**
   - Always provide audio AND haptic feedback
   - Don't rely solely on visual cues
   - Ensure feedback is immediate and clear

2. **User Control**
   - Allow users to adjust feedback intensity
   - Provide options for different accessibility needs
   - Never force users into uncomfortable situations

3. **Error Prevention**
   - Provide clear audio cues for actions
   - Confirm important actions with multiple feedback types
   - Allow easy recovery from mistakes

### Audio Design

```csharp
// Good: Clear, descriptive audio cues
public void PlayPaddleLocationSound()
{
    if (distanceToPaddle < activationDistance)
    {
        audioSource.Play();
    }
}

// Better: Distance-based intensity
public void PlayPaddleLocationSound()
{
    float intensity = 1f - (distanceToPaddle / activationDistance);
    audioSource.volume = Mathf.Clamp01(intensity);
    audioSource.Play();
}
```

### Haptic Design

```csharp
// Good: Clear haptic patterns
public void ProvideHapticFeedback(float intensity)
{
    int[] motorValues = new int[6];
    for (int i = 0; i < motorValues.Length; i++)
    {
        motorValues[i] = Mathf.RoundToInt(intensity * 100);
    }
    BhapticsLibrary.PlayMotors((int)PositionType.GloveR, motorValues, 250);
}
```

## Testing Procedures

### Unit Testing

1. **Test Individual Components**
   ```csharp
   [Test]
   public void BallCollision_WithPaddle_TriggersHapticFeedback()
   {
       // Arrange
       var ball = new Ball();
       var paddle = new GameObject();
       
       // Act
       ball.OnCollisionEnter(new Collision());
       
       // Assert
       Assert.IsTrue(hapticFeedbackWasTriggered);
   }
   ```

2. **Test Accessibility Features**
   ```csharp
   [Test]
   public void AudioGuide_WithinRange_PlaysSound()
   {
       // Test audio guidance functionality
   }
   ```

### Integration Testing

1. **Test Module Interactions**
   - Verify GameManager coordinates modules correctly
   - Test event communication between modules
   - Ensure accessibility features work together

2. **Test VR Interactions**
   - Test hand tracking accuracy
   - Verify controller input handling
   - Test haptic feedback patterns

### Accessibility Testing

1. **Test with BLV Users**
   - Always test new features with actual BLV users
   - Gather feedback on effectiveness
   - Iterate based on user feedback

2. **Test Different Scenarios**
   - Test with various levels of visual impairment
   - Test with different assistive technologies
   - Test in different lighting conditions

### Performance Testing

1. **VR Performance**
   - Ensure 90fps frame rate
   - Test on target hardware (Meta Quest)
   - Monitor memory usage and garbage collection

2. **Accessibility Performance**
   - Test audio latency
   - Verify haptic response time
   - Ensure smooth interaction feedback

## Pull Request Process

### Before Submitting

1. **Test Your Changes**
   - Test in Unity Editor
   - Test on VR hardware
   - Test accessibility features
   - Run performance tests

2. **Follow Code Standards**
   - Add XML documentation
   - Follow naming conventions
   - Ensure proper error handling
   - Add appropriate logging

3. **Update Documentation**
   - Update README if needed
   - Add comments for complex logic
   - Document accessibility features

### Pull Request Template

```markdown
## Description
Brief description of changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Accessibility improvement
- [ ] Performance optimization
- [ ] Documentation update

## Testing
- [ ] Tested in Unity Editor
- [ ] Tested on VR hardware
- [ ] Tested with BLV users
- [ ] Performance tested

## Accessibility Impact
Describe how this change affects BLV users

## Screenshots/Videos
If applicable, add screenshots or videos

## Checklist
- [ ] Code follows project standards
- [ ] Documentation is updated
- [ ] Tests are included
- [ ] Accessibility is considered
```

## Issue Reporting

### Bug Reports

When reporting bugs, include:

1. **Environment Details**
   - Unity version
   - VR headset model
   - Operating system
   - Hardware specifications

2. **Steps to Reproduce**
   - Clear, step-by-step instructions
   - Expected vs actual behavior
   - Screenshots or videos if applicable

3. **Accessibility Context**
   - How the bug affects BLV users
   - Whether it impacts accessibility features
   - Severity for different user types

### Feature Requests

When requesting features:

1. **Describe the Need**
   - What problem does this solve?
   - How does it benefit BLV users?
   - What's the expected user experience?

2. **Consider Implementation**
   - Is it technically feasible?
   - Does it fit the project architecture?
   - What are the accessibility implications?

3. **Provide Examples**
   - Similar features in other applications
   - Mockups or prototypes
   - User research or feedback

## Getting Help

### Resources

- **Unity Documentation**: https://docs.unity3d.com/
- **XR Interaction Toolkit**: https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.5/manual/index.html
- **Accessibility Guidelines**: https://www.w3.org/WAI/WCAG21/quickref/
- **VR Accessibility**: https://www.w3.org/TR/xaur/

### Community

- **GitHub Issues**: For technical questions and bug reports
- **Email**: For sensitive accessibility feedback: xability-lab@illinois.edu


## Recognition

Contributors will be recognized in:

- Project README
- Release notes
- Community acknowledgments

Thank you for contributing to making VR gaming more accessible for the BLV community! 